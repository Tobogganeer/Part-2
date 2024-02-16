using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 4f;
    public float turnSpeed = 180f;
    public float acceleration = 5f;

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
        // TODO: Implement
    }

    void SetTargetRotation()
    {
        /*
        Vector3 cursorPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0; // Make it 2D

        // Direction from us to the mouse
        Vector2 direction = cursorPosition - transform.position;
        // Angle from us to the mouse (in degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Where we want to be headed (convert to unity coord system)
        targetRotation = Quaternion.Euler(0, 0, angle - 90f);
        */
    }

    void UpdateLifetime()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Explode();
        }
        else
        {

        }

        /*
        float multiplier = 1f;

        // Check if we are boosting currently
        if (IsBoosting)
        {
            boostTimer -= Time.deltaTime;
            float normalizedTime = (boostTime - boostTimer) / boostTime; // Remap timer to be 0-1
            float curveValue = boostCurve.Evaluate(normalizedTime); // Curve goes from 0-1
            multiplier = Mathf.Lerp(1f, boostMultiplier, curveValue);
            HUD.SetBoostTime(normalizedTime); // Update the HUD bar (going up)
        }
        // Lower the cooldown if necessary
        else if (boostCooldownTimer > 0)
        {
            boostCooldownTimer -= Time.deltaTime;
            HUD.SetBoostTime(boostCooldownTimer / boostCooldown); // Update the HUD bar (going down)
        }

        currentBoostMultiplier = multiplier;
        */
    }


    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    void Rotate()
    {
        /*
        Quaternion currentRotation = Quaternion.Euler(0, 0, rb.rotation);
        float t = turnSpeed * currentBoostMultiplier * Time.deltaTime;
        Quaternion smoothedRotation = Quaternion.Slerp(currentRotation, targetRotation, t);
        rb.MoveRotation(smoothedRotation);
        */
    }

    void Move()
    {
        /*
        // The jet wants to keep going forward
        velocity = Vector2.Lerp(velocity, transform.up * speed * currentBoostMultiplier, acceleration * Time.deltaTime);
        // TODO: Account for boost
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
        */
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
- Spawns explosion effect when blowing up (due to plane or time fuse)
- Destroys plane (and self) on collision
- Rigidbody2D for movement
- Animator for smoke and boost animations
- Collider used to detect crash
- Pseudocode:
  - Variables for speed, angularSpeed, velocityCurve, maxLifetime, lifetime,
  - (cont'd) rigidbody, targetRotation, plane, animator, explosionPrefab (overall very similar to plane)
  - ^^^ Maybe missile and plane will inherit from common base class (or composition smth)
  - Gather references to components and plane in Start
  - (cont'd) Update 'missiles spawned' count of HUD/Manager
  - Predict plane position and update target rotation in Update
  - (con't) Increment lifetime and update speeds accordingly (Destroy if max lifetime reached)
  - Rotate and move in FixedUpdate
  - Destroy self and plane in OnTriggerEnter2D with plane
  - fn Explode() -> spawns explosion particles and destroys self, updates missiles dodged score

*/
