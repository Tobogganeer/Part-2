using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Lives")]
    public GameObject[] livesIcons;

    [Header("Boost")]
    public Slider boostSlider;
    public Image boostFill;
    public Color boostingColour;
    public Color boostRechargingColour;
    public Color boostAvailableColour;

    [Header("Clock")]
    public TMP_Text clockText;
    public TMP_Text clockHighScoreText;

    [Header("Score")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private void Start()
    {
        // Get the boost slider ready
        SetBoostCooldown(0f);
    }

    public static void SetBoostTime(float value01)
    {
        instance.boostSlider.value = 1f - value01;
        instance.boostFill.color = instance.boostingColour;
    }

    public static void SetBoostCooldown(float value01)
    {
        instance.boostSlider.value = 1f - value01;
        // If it's all the way recharged, change the colour.
        instance.boostFill.color = value01 < 0.01f ? instance.boostAvailableColour : instance.boostRechargingColour;
    }

    public static void SetLives(int livesLeft)
    {
        // Set the icons active if they are lucky enough
        for (int i = 0; i < instance.livesIcons.Length; i++)
            instance.livesIcons[i].SetActive(i < livesLeft);
    }

    public void SetClock(int time)
    {
        clockText.text = "Clock: " + time;
    }

    public void SetClockHighScore(int time)
    {
        clockHighScoreText.text = $"(High: {time})";
    }

    public void SetScore(int missilesDodged)
    {
        scoreText.text = "Score: " + missilesDodged;
    }

    public void SetHighScore(int missilesDodged)
    {
        highScoreText.text = $"(High: {missilesDodged})";
    }
}

/*

HUD.cs (heads-up display, score and such)
- Manages the UI like score, lives, etc
- No components in and of itself
- Pseudocode:
  - Variables for livesText, scoreText, highScoreText, missilesDodgedText etc, playerRespawnText, singleton
  - Stores singleton in Awake
  - static fns for SetScore, SetMissileDodges, SetLives, etc which update UI elements

*/