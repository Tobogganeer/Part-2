using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static SubbuteoPlayer SelectedPlayer { get; private set; }
    public static int Score { get; set; }

    public float flickChargeSpeed = 1.0f; // Charge percent per second
    public float flickPower = 20f;

    [Space]
    public Slider flickPowerSlider;

    float flickCharge;
    Vector2? flickForce;

    public static void SetCurrentPlayer(SubbuteoPlayer player)
    {
        if (SelectedPlayer != null)
            SelectedPlayer.Selected(false);
        SelectedPlayer = player;
        SelectedPlayer.Selected(true);
    }

    private void Update()
    {
        if (SelectedPlayer == null)
        {
            // Don't allow charging if we have no player
            flickCharge = 0;
            UpdateFlickSlider();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flickCharge = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            flickCharge += flickChargeSpeed * Time.deltaTime;
            flickCharge = Mathf.Min(flickCharge, 1f); // Clamp to never be greater than 1
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            float force = flickPower * flickCharge;
            // Find out where they are going
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = cursorPos - SelectedPlayer.transform.position;
            // Set the total force we will be applying to the player
            flickForce = mouseDir.normalized * force;

            // Set us back to 0 (I know GetKeyDown does this but still)
            flickCharge = 0;
        }

        UpdateFlickSlider();
    }

    private void FixedUpdate()
    {
        // Only advance if we actually have a player
        if (SelectedPlayer == null)
            return;

        if (flickForce != null)
        {
            // Move the player and reset the force
            SelectedPlayer.Move(flickForce.Value);
            flickForce = null;
        }
    }

    private void UpdateFlickSlider()
    {
        flickPowerSlider.value = flickCharge;
    }
}
