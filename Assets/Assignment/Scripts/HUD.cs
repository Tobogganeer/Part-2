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
    public Color boostAvailableColour;
    public Color boostRechargingColour;
    public Color boostNeutralColour;

    [Header("Clock")]
    public TMP_Text clockText;

    [Header("Score")]
    public TMP_Text scoreText;

    public static void SetBoostTime(float value01)
    {

    }

    public static void SetBoostCooldown(float value01)
    {

    }

    public static void SetClock()
    {

    }

    public void SetLives(int livesLeft)
    {

    }

    public void SetScore(int missilesDodged)
    {

    }
}
