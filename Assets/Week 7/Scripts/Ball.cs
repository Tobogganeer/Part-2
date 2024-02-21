using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform kickoff;
    Rigidbody2D rb;
    static readonly string GoalTag = "Goal";

    bool isCopy = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isCopy = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Make sure we are colliding with the goal
        if (!collision.CompareTag(GoalTag) || isCopy) return;

        // Increase score
        Controller.Score++;
        // Reset ourselves
        transform.position = kickoff.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
}
