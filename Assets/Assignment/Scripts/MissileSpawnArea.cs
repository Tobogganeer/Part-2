using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnArea : MonoBehaviour
{
    public Vector2 spawnArea;
    public float actualSpawnPushback = 5f;

    public Vector2 GetRandomPosition()
    {
        float x = Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
        float y = Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f);
        return transform.position + new Vector3(x, y);
    }

    public Vector2 GetMissilePosition(Vector2 spawnPosition)
    {
        // Direction from origin to point is just the point normalized
        return spawnPosition + spawnPosition.normalized * actualSpawnPushback;
    }

    private void OnDrawGizmos()
    {
        // Our spawn area
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnArea);

        // Calculate where missiles will spawn
        Vector2 pushCenter = transform.position;
        Bounds b = new Bounds(pushCenter, spawnArea);
        Vector2 ext = b.extents;

        // Find some good points and reset the bounds to that
        Vector2 min = GetMissilePosition(b.min);
        Vector2 max = GetMissilePosition(b.max);
        b.SetMinMax(min, max);

        // Push all 4 corners away (we've already done two but meh)
        b = PushCorner(b, pushCenter, new Vector2(ext.x, ext.y));
        b = PushCorner(b, pushCenter, new Vector2(-ext.x, ext.y));
        b = PushCorner(b, pushCenter, new Vector2(-ext.x, -ext.y));
        b = PushCorner(b, pushCenter, new Vector2(ext.x, -ext.y));

        // Draw that area
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(b.center, b.size);
    }

    Bounds PushCorner(Bounds b, Vector2 center, Vector2 cornerExtents)
    {
        Vector2 pushedCorner = GetMissilePosition(center + cornerExtents);
        b.Encapsulate(pushedCorner);
        return b;
    }
}

/*

MissileSpawnArea.cs
- Stores where missile warnings and missiles can be spawned DONE
- Just a vector2 for the area and a float for how far back missiles should be spawned DONE
- Pseudocode
  - Variables for areaSize, missilePushback DONE
  - fn GetRandomPosition() -> returns random point in area (will be onscreen) DONE
  - fn GetMissilePosition(spawnPos) -> pushes spawnPos back by missilePushback (will be offscreen) DONE
  - Draw helpful visuals in OnDrawGizmos DONE

*/