using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    public Rigidbody2D goalkeeperRB;
    public float goalRadius = 2.5f;

    private void FixedUpdate()
    {
        // Default target is the centre of the goal
        Vector2 targetPosition = transform.position;

        if (Controller.SelectedPlayer != null)
        {
            // Calculate the direction and distance to the player from the goal centre
            Vector2 playerPos = Controller.SelectedPlayer.transform.position;
            Vector2 centerPos = transform.position;
            Vector2 direction = playerPos - centerPos;
            float distance = direction.magnitude;

            // Set the target position (according to the diagram)
            targetPosition = centerPos + direction.normalized * Mathf.Min(goalRadius, distance / 2f);
        }

        goalkeeperRB.position = targetPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, goalRadius);
    }
}
