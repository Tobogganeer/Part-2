using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnArea : MonoBehaviour
{
    public Vector2 spawnArea;
    public float actualSpawnPushback = 5f;
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