using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static SubbuteoPlayer SelectedPlayer { get; private set; }

    public float flickChargeSpeed = 0.33f; // 3 seconds to charge?
    public float flickPower = 100f;

    [Space]
    public Slider flickPowerSlider;

    float flickCharge;

    public static void SetCurrentPlayer(SubbuteoPlayer player)
    {
        if (SelectedPlayer != null)
            SelectedPlayer.Selected(false);
        SelectedPlayer = player;
        SelectedPlayer.Selected(true);
    }

    private void Update()
    {
        
    }
}
