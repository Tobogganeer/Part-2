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

    public int highScore;
    public int highTime;

    float clockTimer;

    void Start()
    {
        LoadScores();
        // Initialize the texts to their default values
        UpdateClockUI();
        UpdateScoreUI();
        UpdateHighScores();
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
            UpdateHighScores();
        }
    }

    void UpdateClockUI()
    {
        SendMessage(nameof(HUD.SetClock), clock);
    }

    void UpdateScoreUI()
    {
        SendMessage(nameof(HUD.SetScore), missilesDodged);
    }


    public static void OnMissileDodged()
    {
        instance.missilesDodged++;
        instance.UpdateScoreUI();
        instance.UpdateHighScores();
    }



    void UpdateHighScores()
    {
        // Check if our current scores are higher
        highScore = Mathf.Max(highScore, missilesDodged);
        highTime = Mathf.Max(highTime, clock);

        // Update the UI
        SendMessage(nameof(HUD.SetHighScore), highScore);
        SendMessage(nameof(HUD.SetClockHighScore), highTime);
    }

    void LoadScores()
    {
        highScore = PlayerPrefs.GetInt(nameof(highScore), 0);
        highTime = PlayerPrefs.GetInt(nameof(highTime), 0);
    }

    void SaveScores()
    {
        PlayerPrefs.SetInt(nameof(highScore), highScore);
        PlayerPrefs.SetInt(nameof(highTime), highTime);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        // Save our high scores before we leave
        SaveScores();
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
