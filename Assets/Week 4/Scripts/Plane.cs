using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float pointThreshold = 0.33f;

    [Space]
    public float moveSpeed = 1.0f;
    public float reachedPointThreshold = 0.1f;
    public float rotationSpeed = 360f;

    [Space]
    public AnimationCurve landingCurve;
    public float landingTime = 1.0f;

    [Space]
    public GameObject proximityWarningObject;

    [HideInInspector]
    // Modified by ProximitySensor
    public int nearbyPlanes;

    Quaternion targetRotation;
    Vector3 lastPosition;
    Vector3 startingScale;
    float landingTimer;

    Camera cam;
    LineRenderer lineRenderer;
    Rigidbody2D rb;

    static readonly string PlaneTag = "Plane";

    private void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        rb = GetComponent<Rigidbody2D>();

        startingScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            landingTimer += (1f / landingTime) * Time.deltaTime;
            float interpolation = landingCurve.Evaluate(landingTimer);

            // Whoops, we landed
            if (landingTimer >= 1.0f)
                Destroy(gameObject);
            else
                // Landing timer @ 0 means interpolation will be high
                transform.localScale = Vector3.Lerp(Vector3.zero, startingScale, interpolation);
        }

        // Turn the warning light on if we are near any planes
        proximityWarningObject.SetActive(nearbyPlanes > 0);
    }

    private void FixedUpdate()
    {
        Vector2 current = transform.position;
        Vector2 target;

        if (points.Count > 0)
            target = points[0];
        else
            target = current + (Vector2)transform.up;

        // Calculate where our heading should be
        Vector2 targetDirection = target - current;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, 0, targetAngle - 90);

        // Rotate us towards our target
        Quaternion updatedRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.MoveRotation(updatedRotation);

        // Move us forwards
        Vector2 updatedPosition = current + (Vector2)transform.up * moveSpeed * Time.deltaTime;
        rb.MovePosition(updatedPosition);

        // If we are going towards a point and have reached it
        if (points.Count > 0 && Vector2.Distance(rb.position, target) < reachedPointThreshold)
        {
            // Remove the first point
            points.RemoveAt(0);

            // Get the positions of the line renderer (should be same as our list?)
            Vector3[] lrPoints = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(lrPoints);

            // Convert to List for RemoveAt method
            List<Vector3> pointList = new List<Vector3>(lrPoints);
            pointList.RemoveAt(0);
            // Reset it to the smaller list
            lineRenderer.positionCount = pointList.Count;
            lineRenderer.SetPositions(pointList.ToArray());

            // Was going to manually do it and then create a smaller array,
            //  but the list is a bit simpler
            /*
            for (int i = lrPoints.Length - 2; i >= 0; i--)
            {
                // Shift all points down one
                lrPoints[i] = lrPoints[i + 1];
            }
            */
            //lineRenderer.SetPositions()
        }
    }

    private void OnBecameInvisible()
    {
        // Kill us if we leave the screen
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlaneTag))
            Destroy(gameObject);
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
