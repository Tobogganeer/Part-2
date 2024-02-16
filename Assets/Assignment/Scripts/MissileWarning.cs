using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWarning : MonoBehaviour
{
    public float warningTime;
    public GameObject missilePrefab;

    Vector2 missileSpawnPos;
    float timer;

    // Called when spawned by MissileSpawner, essentially Start()
    public void Init(Vector2 missileSpawnPos)
    {
        this.missileSpawnPos = missileSpawnPos;
        timer = warningTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Vector2 targetPosition = Vector2.zero;
        // Spawn facing the plane if it's alive
        if (MissileDodgeManager.JetAlive)
            targetPosition = MissileDodgeManager.CurrentJet.transform.position;

        // Seen this code a million times already
        Vector2 targetDir = targetPosition - missileSpawnPos;
        float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion spawnRot = Quaternion.Euler(0, 0, targetAngle - 90f);

        Instantiate(missilePrefab, missileSpawnPos, spawnRot);
        Destroy(gameObject);
    }
}

/*

MissileWarning.cs
- Spawned by MissileSpawner
- Blinks for a few seconds to warn player of incoming missile
- Spawns missile after said few seconds
- Animator for blinking animation
- Pseudocode:
  - Variables for animator, missilePrefab, warningTime, timer, missileSpawnPos
  - Gather references to animator and set timer in Start (may not even be needed really)
  - Decrement timer in update and spawn missile/destroy self if necessary
  - fn Spawn() -> does the above (spawns missile and destroys self)
  - fn Init(spawnPos) -> sets the missile spawn position

*/