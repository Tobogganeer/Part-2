using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnArea : MonoBehaviour
{
    public Vector2 spawnArea;
    public float actualSpawnPushback = 5f;

    public Vector2 GetRandomPosition()
    {
        float x = Random.Range(-spawnArea.x, spawnArea.x);
        float y = Random.Range(-spawnArea.y, spawnArea.y);
        return transform.position + new Vector3(x, y);
    }

    public Vector2 GetMissilePosition(Vector2 spawnPosition)
    {
        // Direction from origin to point is just the point normalized
        return spawnPosition + spawnPosition.normalized * actualSpawnPushback;
    }
}

/*

MissileSpawnArea.cs
- Stores where missile warnings and missiles can be spawned
- Just a vector2 for the area and a float for how far back missiles should be spawned
- Pseudocode
  - Variables for areaSize, missilePushback
  - fn GetRandomPosition() -> returns random point in area (will be onscreen)
  - fn GetMissilePosition(spawnPos) -> pushes spawnPos back by missilePushback (will be offscreen)
  - Draw helpful visuals in OnDrawGizmos

*/