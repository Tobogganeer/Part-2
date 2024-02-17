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
        lives = startingLives;
        // Show how many lives we start with
        HUD.SetLives(lives);
    }

    public static void OnPlaneDestroyed()
    {
        // Take away a life and update the display
        instance.lives--;
        HUD.SetLives(instance.lives);

        instance.respawnTimer = instance.respawnTime;
    }

    private void Update()
    {
        if (lives <= 0)
        {
            OutOfLives();
            return;
        }

        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
            // Show how much time we have left
            respawnTimerText.gameObject.SetActive(true);
            respawnTimerText.text = "Respawn in " + Mathf.Ceil(respawnTimer);

            if (respawnTimer <= 0)
            {
                // Just reached 0, spawn back in
                SpawnJet();
            }
        }
        else
        {
            respawnTimerText.gameObject.SetActive(false);
        }
    }

    void OutOfLives()
    {
        // Turn off the normal UI and display the game over screen
        respawnTimerText.gameObject.SetActive(false);
        hud.SetActive(false);
        gameOverScreen.SetActive(true);
        // Display your stats for this run
        scoreText.text = "Score: " + Score.instance.missilesDodged;
        clockText.text = "Time survived: " + Score.instance.clock;
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