using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 4f;
    public float turnDegreesPerSecond = 180f;
    public float acceleration = 5f;
    public float leadTimeMultiplier = 1.2f; // Makes the missile predict positions a bit further

    [Space]
    public float maxLifetime = 2f;
    public float velocityMultiplier = 2f;
    public AnimationCurve velocityOverLifetime;

    [Space]
    public GameObject explosionPrefab;

    Rigidbody2D rb;

    float lifetime;
    float currentSpeedMultiplier;

    Quaternion targetRotation;
    Vector2 velocity;
    Vector2 predictedImpactPoint;

    static readonly string Tag_Jet = "Jet";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // The clock is ticking for these missiles
        lifetime = maxLifetime;
    }

    void Update()
    {
        // Predict where the jet will be
        CalculateImpactPoint();
        // Turn towards said point
        SetTargetRotation();
        // Tick the clock (updates speed)
        UpdateLifetime();
    }

    void CalculateImpactPoint()
    {
        if (!MissileDodgeManager.JetAlive)
            return;
        Vector2 jetPos = MissileDodgeManager.CurrentJet.transform.position;
        Vector2 jetVelocity = MissileDodgeManager.CurrentJet.Velocity;

        Vector2 displacement = jetPos - (Vector2)transform.position;
        // Will not take future speed into account! I want to be a bit inaccurate...
        float timeToArrival = displacement.magnitude / speed * currentSpeedMultiplier;
        predictedImpactPoint = jetPos + jetVelocity * (timeToArrival * leadTimeMultiplier);
    }

    void SetTargetRotation()
    {
        // Direction from us to the impact point
        Vector2 direction = predictedImpactPoint - (Vector2)transform.position;
        // Angle from us to the point (in degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Where we want to be headed (convert to unity coord system)
        targetRotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void UpdateLifetime()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Explode();
            return;
        }

        float normalizedLifetime = (maxLifetime - lifetime) / maxLifetime; // Remap lifetime to be 0-1
        float curveValue = velocityOverLifetime.Evaluate(normalizedLifetime); // Curve goes from 0-1
        currentSpeedMultiplier = Mathf.Lerp(1f, velocityMultiplier, curveValue); // Set current speed
    }


    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    void Rotate()
    {
        Quaternion currentRotation = Quaternion.Euler(0, 0, rb.rotation);
        float t = turnDegreesPerSecond * Time.deltaTime; // Maybe multiply by speed multiplier?
        // Unlike the jet, we rotate linearly instead
        Quaternion updatedRotation = Quaternion.RotateTowards(currentRotation, targetRotation, t);
        rb.MoveRotation(updatedRotation);
    }

    void Move()
    {
        // We accelerate forward instead of instantly changing velocity to go forward
        velocity = Vector2.Lerp(velocity, transform.up * speed * currentSpeedMultiplier, acceleration * Time.deltaTime);
        // Actually move
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        // Crashed into a jet (we did our job!)
        if (otherObj.CompareTag(Tag_Jet))
        {
            Explode();
            Destroy(otherObj.gameObject);
        }
    }

    public void Explode()
    {
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // Increase the score if we didn't blow up the jet
        if (MissileDodgeManager.JetAlive)
            Score.OnMissileDodged();
    }
}

/*

Missile.cs
- Spawned outside of screen
- Tracks towards where plane will be (predicts position)
- Turns slowly (can be out-maneuvered)
- Gains speed over time before exploding after some time
- Spawns explosion effect when blowing up (due to plane or time fuse) DONE
- Destroys plane (and self) on collision DONE
- Rigidbody2D for movement DONE
- Animator for smoke and boost animations DONE
- Collider used to detect crash DONE
- Pseudocode:
  - Variables for speed, angularSpeed, velocityCurve, maxLifetime, lifetime, DONE
  - (cont'd) rigidbody, targetRotation, plane, animator, explosionPrefab (overall very similar to plane) DONE
  - ^^^ Maybe missile and plane will inherit from common base class (or composition smth) NAH
  - Gather references to components and plane in Start DONE
  - (cont'd) Update 'missiles spawned' count of HUD/Manager DONE(ish)
  - Predict plane position and update target rotation in Update
  - (con't) Increment lifetime and update speeds accordingly (Destroy if max lifetime reached)
  - Rotate and move in FixedUpdate
  - Destroy self and plane in OnTriggerEnter2D with plane DONE
  - fn Explode() -> spawns explosion particles and destroys self, updates missiles dodged score DONE

*/
