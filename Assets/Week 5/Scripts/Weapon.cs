using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float damage = 10f;

    Rigidbody2D rb;
    Vector2 movement;

    private void Start()
    {
        // Destroy after a while just in case
        Destroy(gameObject, 15f);
        rb = GetComponent<Rigidbody2D>();
        // Store this to avoid calculating every physics step
        movement = transform.up * moveSpeed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Damage whatever we crash into
        collision.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
