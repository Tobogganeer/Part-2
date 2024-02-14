using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScaler : MonoBehaviour
{
    public void SetTo16x9()
    {
        //SetResolution();
    }

    public void SetToFullHD()
    {
        SetResolution(1920, 1080);
    }

    void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, false);
    }
}
