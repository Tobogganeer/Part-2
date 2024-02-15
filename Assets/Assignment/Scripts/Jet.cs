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

    float boostTimer;
    float boostCooldownTimer;
    Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
