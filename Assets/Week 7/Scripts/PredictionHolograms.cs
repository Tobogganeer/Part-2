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
        // Move holograms off screen
        ballHologram.position = Vector3.left * 1000;
        playerHologram.position = Vector3.left * 1000;

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
        // They don't call Start() and so don't reference their rb; move them directly
        // (instead of SubbuteoPlayer.Move())
        Rigidbody2D copyRB = playerCopy.GetComponent<Rigidbody2D>();
        copyRB.AddForce(force * copyRB.mass, ForceMode2D.Impulse);

        // Time warp!
        scene.Simulate(extrapolationTime);

        ballHologram.position = ballCopy.transform.position;
        playerHologram.position = playerCopy.transform.position;

        Destroy:
        scene.Destroy();
    }
}
