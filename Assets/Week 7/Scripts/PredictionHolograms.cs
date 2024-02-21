using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionHolograms : MonoBehaviour
{
    // Shows holograms of what will happen in the future
    public GameObject ball;
    public float extrapolationTime = 1f;

    [Space]
    public GameObject ballHologram;
    public GameObject playerHologram;

    private void FixedUpdate()
    {
        SimulatedScene scene = PhysicsPrediction.CreateSimulation();
        scene.Simulate(extrapolationTime);

        if (scene.TryGetCopiedObject(ball, out GameObject ballCopy))
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
