using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float pointThreshold = 1.0f;

    Vector3 lastPosition;
    Camera cam;
    LineRenderer lineRenderer;

    private void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    private void OnMouseDown()
    {
        points.Clear();
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        lastPosition = mousePos;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, lastPosition);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if ((mousePos - lastPosition).sqrMagnitude > pointThreshold * pointThreshold)
        {
            points.Add(mousePos);
            lineRenderer.SetPosition(lineRenderer.positionCount++, mousePos);
            lastPosition = mousePos;
        }
    }
}
