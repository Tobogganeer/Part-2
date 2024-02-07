using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float speed = 2f;
    public float maxHealth = 100f;

    Rigidbody2D rb;
    Animator animator;
    Camera cam;

    Vector2 destination;
    float health;
    bool moving;

    const float DestinationDistanceThreshold = 0.05f; // 5cm is close enough

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = Camera.main; // I know it's cached internally now...

        health = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            destination = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        // Move towards our destination, but don't go past it
        Vector2 newPosition = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        // Check if we still have a bit to move
        moving = (newPosition - destination).sqrMagnitude > DestinationDistanceThreshold * DestinationDistanceThreshold;

        if (moving)
            rb.MovePosition(newPosition);
    }
}
