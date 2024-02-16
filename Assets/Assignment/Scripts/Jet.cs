using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Jet : MonoBehaviour
{
    public float speed = 4f;
    public float turnSpeed = 3f;
    public float acceleration = 5f;

    [Space]
    public float boostTime = 2f;
    public float boostMultiplier = 2f;
    public AnimationCurve boostCurve;
    public float boostCooldown = 6.5f;

    public bool IsBoosting => boostTimer > 0;
    public Vector2 Velocity => velocity;

    Rigidbody2D rb;
    Animator animator;
    Camera cam;

    float boostTimer;
    float boostCooldownTimer;
    float currentBoostMultiplier;

    Quaternion targetRotation;
    Vector2 velocity;

    float lastRotation;
    float angularVelocity;

    static readonly int Anim_AngularVelocity = Animator.StringToHash("rotation");


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
        lastRotation = rb.rotation;
    }

    void Update()
    {
        // Tell the animator what direction we are turning
        SetAnimatorRotation();
        // Find where we should be turning
        SetTargetRotation();
        // Update the boost
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryStartBoost();
        ApplyBoost();
    }

    void SetTargetRotation()
    {
        Vector3 cursorPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0; // Make it 2D

        // Direction from us to the mouse
        Vector2 direction = cursorPosition - transform.position;
        // Angle from us to the mouse (in degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Where we want to be headed (convert to unity coord system)
        targetRotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void SetAnimatorRotation()
    {
        animator.SetFloat(Anim_AngularVelocity, angularVelocity);
    }

    void TryStartBoost()
    {
        if (boostTimer <= 0 && boostCooldownTimer <= 0)
        {
            // Reset the two timers
            boostTimer = boostTime;
            boostCooldownTimer = boostCooldown;
        }
    }

    void ApplyBoost()
    {
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
            HUD.SetBoostCooldown(boostCooldownTimer / boostCooldown); // Update the HUD bar (going down)
        }

        currentBoostMultiplier = multiplier;
    }


    void FixedUpdate()
    {
        angularVelocity = (lastRotation - rb.rotation) / Time.deltaTime;
        Rotate();
        Move();
        // Set this to detect turn direction
        lastRotation = rb.rotation;
    }

    void Rotate()
    {
        Quaternion currentRotation = Quaternion.Euler(0, 0, rb.rotation);
        float t = turnSpeed * currentBoostMultiplier * Time.deltaTime;
        Quaternion smoothedRotation = Quaternion.Slerp(currentRotation, targetRotation, t);
        rb.MoveRotation(smoothedRotation);
    }

    void Move()
    {
        // The jet wants to keep going forward
        velocity = Vector2.Lerp(velocity, transform.up * speed * currentBoostMultiplier, acceleration * Time.deltaTime);
        // TODO: Account for boost
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    void OnDestroy()
    {
        MissileDodgeManager.OnPlaneDestroyed();
    }
}

/*

Jet.cs (player)
- Goes towards cursor location DONE
- Has a temporary speed boost on click DONE
- Turns to face the cursor and flies forward DONE
- Has limited number of lives (moved to manager)
- By nature of following mouse will never fly too far out of bounds DONE
- Rigidbody2D (kinematic) for movement and collisions DONE
- Some kind of 2D collider (maybe capsule) DONE
- Animator for plane animations (maybe wiggles wings when turning) DONE
- Pseudocode:
  - Variables for speed, angularSpeed, boostTime, boostCurve, boostCooldown, boostTimer, DONE
  - (cont'd) rigidbody, targetRotation, animator DONE
  - Gather references to components in Start DONE
  - Update target rotation and animator in Update DONE
  - (cont'd) Check for boost in Update and update HUD DONE
  - Rotate and move in FixedUpdate DONE
  - Let Manager know when this is destroyed DONE

*/
