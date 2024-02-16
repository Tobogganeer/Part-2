using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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