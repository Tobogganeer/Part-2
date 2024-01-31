using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float pointThreshold = 1.0f;

    Vector3 lastPosition;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        points.Clear();
        lastPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if ((mousePos - lastPosition).sqrMagnitude > pointThreshold * pointThreshold)
        {
            points.Add(mousePos);
            lastPosition = mousePos;
        }
    }
}
