using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject planePrefab;
    public Vector2 spawnTimeRange = new Vector2(5f, 10f);
    public Vector2 scaleRange = new Vector2(0.8f, 1.4f);
    public Vector2 speedMultiplierRange = new Vector2(0.75f, 1.75f);

    [Space]
    //public Vector2 saturationRange = new Vector2(0.4f, 0.7f);
    //public Vector2 valueRange = new Vector2(0.4f, 0.7f);
    public Sprite[] planeSprites;

    [Space]
    public Vector2 spawnArea = Vector2.one;
    public float planeTargetRadius = 5f;

    float timer;

    private void Start()
    {
        timer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    private void Update()
    {
        // The planning said to increment, but this uses one less variable
        timer -= Time.deltaTime;

        if (timer  < 0)
        {
            // Reset timer
            timer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            Vector2 spawnPosition = transform.position;
            // Generate random position in spawn area
            Vector2 extents = spawnArea / 2f;
            spawnPosition.x += Random.Range(-extents.x, extents.x);
            spawnPosition.y += Random.Range(-extents.y, extents.y);

            // Where we will point the plane
            Vector2 target = Random.insideUnitCircle * planeTargetRadius;
            Vector2 targetDir = target - spawnPosition;
            float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            // Spawn it
            GameObject plane = Instantiate(planePrefab, spawnPosition, Quaternion.Euler(0, 0, targetAngle - 90));
            plane.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
            //plane.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f,
            //    saturationRange.x, saturationRange.y, valueRange.x, valueRange.y);
            plane.GetComponent<SpriteRenderer>().sprite = planeSprites[Random.Range(0, planeSprites.Length)];

            // Randomize speed
            Plane p = plane.GetComponent<Plane>();
            float speedMult = Random.Range(speedMultiplierRange.x, speedMultiplierRange.y);
            p.moveSpeed *= speedMult;
            p.rotationSpeed *= speedMult;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, spawnArea); // Spawn area
        Gizmos.DrawWireSphere(Vector3.zero, planeTargetRadius); // Will fly towards centre of world
        Gizmos.DrawLine(transform.position, Vector3.zero); // Line from here to centre
    }
}
