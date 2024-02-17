using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissileDodgeManager : MonoBehaviour
{
    public static MissileDodgeManager instance;

    public float respawnTime = 5f;
    [Range(1, 4)] public int startingLives = 3;
    public GameObject jetPrefab;
    Jet currentJet;

    [Space]
    public GameObject hud;
    public GameObject gameOverScreen;
    public TMP_Text respawnTimerText;
    public TMP_Text scoreText;
    public TMP_Text clockText;

    float respawnTimer;
    int lives;

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

/*

MissileDodgeManager.cs (various housekeeping)
- Respawns player, forces game end, manages game flow
- Pseudocode:
  - Variables for playerPrefab, lives, respawnTime, respawnTimer, isPlaneAlive
  - fn OnPlaneDestroyed() -> sets timer and isPlaneAlive bool, decreases lives
  - Update respawn timer and corresponding text in Update
  - (cont'd) respawn plane near middle of world when timer has elapsed and lives > 0
  - Show game over text and force exit to main menu if lives <= 0

*/