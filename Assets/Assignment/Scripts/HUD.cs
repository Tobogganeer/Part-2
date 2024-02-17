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

    [Header("Score")]
    public TMP_Text scoreText;

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

    public static void SetClock(int time)
    {
        instance.clockText.text = "Clock: " + time;
    }

    public void SetLives(int livesLeft)
    {
        // Set the icons active if they are lucky enough
        for (int i = 0; i < livesIcons.Length; i++)
            livesIcons[i].SetActive(i < livesLeft);
    }

    public void SetScore(int missilesDodged)
    {
        scoreText.text = "Score: " + missilesDodged;
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