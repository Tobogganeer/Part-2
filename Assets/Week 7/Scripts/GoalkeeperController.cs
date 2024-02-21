using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    public Rigidbody2D goalkeeperRB;
    public float goalRadius = 2.5f;
    public float moveSpeed = 1f;
    public float rotateSpeed = 360f;

    private void FixedUpdate()
    {
        // Default target is the centre of the goal
        Vector2 targetPosition = transform.position;
        Vector2 lookDir = Vector2.down;

        if (Controller.SelectedPlayer != null)
        {
            // Calculate the direction and distance to the player from the goal centre
            Vector2 playerPos = Controller.SelectedPlayer.transform.position;
            Vector2 centerPos = transform.position;
            Vector2 direction = playerPos - centerPos;
            lookDir = direction;
            float distance = direction.magnitude;

            // Set the target position (according to the diagram)
            targetPosition = centerPos + direction.normalized * Mathf.Min(goalRadius, distance / 2f);
        }

        //goalkeeperRB.position = targetPosition;
        goalkeeperRB.MovePosition(Vector2.MoveTowards(goalkeeperRB.position, targetPosition, moveSpeed * Time.deltaTime));

        // Convert target direction into a quaternion
        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, targetAngle + 90f); // + 90 because our goalkeeper is upside down
        // Yes I could rotate the sprite around again but oh well
        goalkeeperRB.MoveRotation(Quaternion.RotateTowards(goalkeeperRB.transform.rotation, targetRot, rotateSpeed * Time.deltaTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, goalRadius);
    }
}
