using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScaler : MonoBehaviour
{
    int width, height;

    public void SetTo16x9()
    {
        // Actually set it to 16x10
        SetResolution(1920, 1200);
    }

    public void SetToFullHD()
    {
        SetResolution(1920, 1080);
    }

    public void SetWidth(int width) => this.width = width;
    public void SetHeight(int height) => this.height = height;

    public void SetResolution(int width, int height)
    {
        SetWidth(width);
        SetHeight(height);
        Apply();
    }

    public void Apply()
    {
        Screen.SetResolution(width, height, false);
    }
}
