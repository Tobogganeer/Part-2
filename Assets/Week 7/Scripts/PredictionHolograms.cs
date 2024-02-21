using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionHolograms : MonoBehaviour
{
    // Shows holograms of what will happen in the future
    public GameObject devBallMarker;
    public GameObject ball;
    public float devExtrapolationTime = 1f;

    private void FixedUpdate()
    {
        SimulatedScene scene = PhysicsPrediction.CreateSimulation();
        scene.TryGetCopiedObject(ball, out GameObject ballCopy);
        ballCopy.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Rigidbody2D>().velocity;
        scene.Simulate(devExtrapolationTime);
        if (scene.TryGetCopiedObject(ball, out ballCopy))
        {
            // Show where the ball will be
            devBallMarker.transform.position = ballCopy.GetComponent<Rigidbody2D>().position;
        }
        else
        {
            Debug.LogWarning("Could not find ball copy!");
        }
        scene.Destroy();
    }
}
