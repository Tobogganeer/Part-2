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

    static readonly int Anim_AngularVelocity = Animator.StringToHash("rotation");


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        // Find where we should be turning
        SetTargetRotation();
        // Tell the animator what direction we are turning
        animator.SetFloat(Anim_AngularVelocity, rb.angularVelocity);
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

    void FixedUpdate()
    {
        rb.MoveRotation(targetRotation);
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
