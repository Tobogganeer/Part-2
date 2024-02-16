using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWarning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Vector2 missileSpawnPos)
    {

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