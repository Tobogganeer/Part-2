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

    Rigidbody2D rb;
    Animator animator;

    float lifetime;
    float currentSpeedMultiplier;

    Quaternion targetRotation;
    Vector2 velocity;
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
