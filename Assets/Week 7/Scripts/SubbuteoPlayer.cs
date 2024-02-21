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

    private void OnMouseDown()
    {
        // We are selected yippee!!!
        Selected(true);
    }
}
