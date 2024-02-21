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
    public Transform ballHologram;
    public Transform playerHologram;

    private void FixedUpdate()
    {
        // Check if we have a player selected
        if (Controller.SelectedPlayer == null)
            return;

        // Check if we aren't even trying to flick
        Vector2 force = Controller.GetCurrentForce();
        if (force.sqrMagnitude < 0.01)
            return;

        SimulatedScene scene = PhysicsPrediction.CreateSimulation();

        if (!scene.TryGetCopiedObject(ball, out GameObject ballCopy))
            goto Destroy;
        if (!scene.TryGetCopiedObject(Controller.SelectedPlayer.gameObject, out GameObject playerCopy))
            goto Destroy;

        // Simulate pushing the player
        playerCopy.GetComponent<SubbuteoPlayer>().Move(force);

        // Time warp!
        scene.Simulate(extrapolationTime);

        ballHologram.position = ballCopy.transform.position;
        playerHologram.position = playerCopy.transform.position;

        Destroy:
        scene.Destroy();
    }
}
