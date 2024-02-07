using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float height = 5f;
    public Direction direction;

    public void SpawnWeapon()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.y += Random.Range(-height / 2f, height / 2f); // Random up/down offset
        Quaternion spawnRotation = Quaternion.Euler(0, 0, direction == Direction.Left ? 90f : -90f);
        Instantiate(prefab, spawnPoint, spawnRotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        float halfHeight = height / 2f;
        Vector3 halfOffset = Vector3.up * halfHeight;
        Vector3 top = transform.position + halfOffset;
        Vector3 bottom = transform.position - halfOffset;

        const float HorizontalOffset = 1f;
        Vector3 lOffset = Vector3.left * HorizontalOffset;
        Vector3 rOffset = Vector3.right * HorizontalOffset;

        Gizmos.DrawLine(top, bottom); // Vertical
        Gizmos.DrawLine(top + lOffset, top + rOffset); // Top cap
        Gizmos.DrawLine(bottom + lOffset, bottom + rOffset); // Bottom cap

        DrawGizmoArrow(direction);
    }

    void DrawGizmoArrow(Direction dir)
    {
        const float ArrowLength = 1.5f;
        const float SidesLength = 1f;

        Vector3 offset = dir == Direction.Left ? Vector3.left : Vector3.right;

        Vector3 tip = transform.position + offset * ArrowLength;
        Gizmos.DrawLine(transform.position, tip); // Going out
        Gizmos.DrawLine(tip, tip + (-offset + Vector3.up).normalized * SidesLength); // Top side
        Gizmos.DrawLine(tip, tip + (-offset + Vector3.down).normalized * SidesLength); // Bottom side
    }

    public enum Direction
    {
        Left,
        Right
    }
}
