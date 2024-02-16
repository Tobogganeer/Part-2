using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetExhaust : MonoBehaviour
{
    public Jet jet;

    Animator animator;

    static readonly int Anim_JetBoosting = Animator.StringToHash("boosting");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(Anim_JetBoosting, jet.IsBoosting);
    }
}
