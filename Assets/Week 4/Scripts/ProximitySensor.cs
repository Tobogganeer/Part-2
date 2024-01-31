using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    public Plane plane;

    static readonly string ProximitySensorTag = "ProximitySensor";

    // Check if planes enter of our warning zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ProximitySensorTag))
            plane.nearbyPlanes++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(ProximitySensorTag))
            plane.nearbyPlanes--;
    }
}
