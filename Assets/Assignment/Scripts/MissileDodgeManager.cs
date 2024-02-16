using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDodgeManager : MonoBehaviour
{
    public static MissileDodgeManager instance;

    public GameObject jetPrefab;
    Jet currentJet;

    public static Jet CurrentJet => instance.currentJet;
    public static bool JetAlive => CurrentJet != null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnJet();
    }

    public static void OnPlaneDestroyed()
    {
        // TODO: Functionality (no exceptions for now)
    }

    void SpawnJet()
    {
        currentJet = Instantiate(jetPrefab, Vector3.zero, Quaternion.identity).GetComponent<Jet>();
    }
}
