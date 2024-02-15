using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    public float speed = 3f;
    public float angularSpeed = 180f;

    [Space]
    public float boostTime = 2f;
    public AnimationCurve boostCurve;
    public float boostCooldown = 6.5f;

    Rigidbody2D rb;
    Animator animator;
    Camera cam;

    float boostTimer;
    float boostCooldownTimer;
    Quaternion targetRotation;
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

    void FixedUpdate()
    {
        angularVelocity = (lastRotation - rb.rotation) / Time.deltaTime;
        rb.MoveRotation(Quaternion.Slerp(Quaternion.Euler(0, 0, rb.rotation), targetRotation, 3f * Time.deltaTime));
        rb.MovePosition(rb.position + (Vector2)transform.up * Time.deltaTime * speed);
        // Set this to detect turn direction
        lastRotation = rb.rotation;
    }
}

/*

Jet.cs (player)
- Goes towards cursor location
- Has a temporary speed boost on click
- Turns to face the cursor and flies forward
- Has limited number of lives
- By nature of following mouse will never fly too far out of bounds
- Rigidbody2D (kinematic) for movement and collisions
- Some kind of 2D collider (maybe capsule)
- Animator for plane animations (maybe wiggles wings when turning)
- Pseudocode:
  - Variables for speed, angularSpeed, boostTime, boostCurve, boostCooldown, boostTimer,
  - (cont'd) rigidbody, targetRotation, animator
  - Gather references to components in Start
  - Update target rotation and animator in Update
  - (cont'd) Check for boost in Update and update HUD
  - Rotate and move in FixedUpdate
  - Let Manager know when this is destroyed

*/
