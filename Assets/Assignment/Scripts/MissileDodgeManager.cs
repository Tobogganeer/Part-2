using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDodgeManager : MonoBehaviour
{
    public static MissileDodgeManager instance;
    
    Jet currentJet;

    public static Jet CurrentJet => instance.currentJet;

    private void Awake()
    {
        instance = this;
    }

    public static void OnPlaneDestroyed()
    {
        // TODO: Functionality (no exceptions for now)
    }
}
