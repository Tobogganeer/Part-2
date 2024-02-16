using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int frames = 14;
    public int frameRate = 60;

    void Start()
    {
        // Destroy ourselves after the animation is done
        Destroy(gameObject, (float)frames / frameRate);
    }
}
