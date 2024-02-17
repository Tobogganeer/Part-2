using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HUD))]
public class Score : MonoBehaviour
{
    public static Score instance;
    private void Awake()
    {
        instance = this;
    }

    public int missilesDodged;
    public int clock;

    float clockTimer;

    void Start()
    {
        // Initialize the texts to their default values
        UpdateClockUI();
    }

    void Update()
    {
        // Count how long we're alive for
        if (MissileDodgeManager.JetAlive)
            clockTimer += Time.deltaTime;

        if (clockTimer > 1.0f)
        {
            clock++;
            clockTimer = 0;
            // Update the hud with the current clock
            UpdateClockUI();
        }
    }

    void UpdateClockUI()
    {
        
        SendMessage(nameof(HUD.SetClock), clock);
    }


    public static void OnMissileDodged()
    {
        // TODO: Functionality (no exceptions for now)
    }
}

/*

Score.cs
- Saves and loads high scores upon start and end of scene
- Functions for updating scores which updates the relevant UI
- Pseudocode:
  - Variables for various scores (see above), singleton
  - Stores singleton in Awake
  - Loads any saved values in Start and updates HUD
  - Saves values in OnDestroy
  - static fns for IncreaseScore, OnLifeLost, OnMissileDodged, etc

*/
