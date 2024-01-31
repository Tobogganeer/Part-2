using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Runway : MonoBehaviour
{
    public int score;

    new BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Make sure this is a plane colliding with us
        if (!collision.CompareTag(Plane.PlaneTag))
            return;

        // Yarrr they be on us matey
        if (collider.OverlapPoint(collision.transform.position))
        {
            Plane p = collision.GetComponent<Plane>();

            // They are already landing, ignore them
            if (p.isLanding)
                return;

            p.isLanding = true;
            score++;
        }
    }
}
