using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubbuteoPlayer : MonoBehaviour
{
    public Color unselectedColour = Color.red;
    public Color selectedColour = Color.green;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set our default colour
        Selected(false);
    }

    public void Selected(bool selected)
    {
        spriteRenderer.color = selected ? selectedColour : unselectedColour;
    }

    public void Move(Vector2 force)
    {
        // Multiply by mass to have a constant velocity change regardless of mass
        rb.AddForce(force * rb.mass, ForceMode2D.Impulse);
    }

    private void OnMouseDown()
    {
        // We are selected yippee!!!
        Controller.SetCurrentPlayer(this);
    }
}
