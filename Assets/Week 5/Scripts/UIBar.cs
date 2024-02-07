using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Image fill;
    public float fillMoveSpeed = 10f;

    float fillTarget = 1f;
    float fillAmount;

    private void Start()
    {
        fillAmount = fillTarget;
    }

    void Update()
    {
        // Make the bar jerk towards desired fill amount
        fillAmount = Mathf.Lerp(fillAmount, fillTarget, fillMoveSpeed * Time.deltaTime);
        fill.fillAmount = fillAmount;
    }

    public void SetFill(float amount01, bool noLerp = false)
    {
        fillTarget = Mathf.Clamp(amount01, 0f, 1f);
        if (noLerp)
            fillAmount = fillTarget;
    }

    public void SetFill(float amount, float min, float max, bool noLerp = false)
    {
        SetFill(Mathf.InverseLerp(min, max, amount), noLerp);
    }
}
