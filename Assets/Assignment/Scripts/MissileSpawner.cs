using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public Vector2 spawnTimeRange = new Vector2(2f, 6.5f);
    public List<MissileSpawnArea> spawnAreas;
    public GameObject missileWarningPrefab;

    float spawnTimer = 3f; // Initial 3 seconds

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && MissileDodgeManager.JetAlive)
        {
            spawnTimer = Random.Range(spawnTimeRange.x, spawnTimeRange.y);)
            SpawnMissileWarning();
        }
    }

    void SpawnMissileWarning()
    {
        MissileSpawnArea area = spawnAreas[Random.Range(0, spawnAreas.Count)];
        Vector2 warningSpawnPos = area.GetRandomPosition();
        Vector2 missileSpawnPos = area.GetMissilePosition(warningSpawnPos);

        MissileWarning warning = Instantiate(missileWarningPrefab,
            warningSpawnPos, Quaternion.identity).GetComponent<MissileWarning>();
        warning.Init(missileSpawnPos);
    }
}

/*

MissileSpawner.cs
- Activates a missile warning and spawns a missile every few seconds (randomized)
- Stores a list of spawn areas (boxes) and spawns the missile in a random area of a random box
- No extra components (empty gameobject)
- Pseudocode:
  - Variables for missileSpawnTimeRange, spawnTimer, spawnAreas, missileWarningPrefab
  - Update timer and spawn missile warning if necessary in Update (see below)
  - fn SpawnMissileWarning() ->
    - Pick random spawn area
    - Pick random position from selected area (will be inside screen)
    - Push position backwards to find missile spawn position
    - Spawn missile warning at position (in screen), passing it the missile position
    - Missile warning handles spawn of missile itself

*/