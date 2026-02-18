using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySummoner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    public void SummonEnemies(DungeonSO data, HashSet<Vector2Int> room, ETypeRoom typeRoom)
    {
      
        Vector2Int center = GetRoomCenter(room);
        Vector3 spawnPos = new Vector3(center.x + 0.5f, center.y + 0.5f, 0);
        if (typeRoom == ETypeRoom.BossRoom)
        {
            Instantiate(data.Bosses[Random.Range(0, data.Bosses.Count)], spawnPos, Quaternion.identity);
            return;
        }

        int enemyCount = Random.Range(data.minEnemiesInRoom, data.maxEnemiesInRoom);

        foreach (var pos in room.OrderBy(x => Guid.NewGuid()).Take(enemyCount))
        {
            _enemies.Add(Instantiate(data.EnemyList[Random.Range(0, data.EnemyList.Count)],
                new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0),
                Quaternion.identity));
            
        }
    }
    private Vector2Int GetRoomCenter(HashSet<Vector2Int> room)
    {
        int x = 0;
        int y = 0;

        foreach (var pos in room)
        {
            x += pos.x;
            y += pos.y;
        }

        return new Vector2Int(x / room.Count, y / room.Count);
    }
    public void ClearAllEnemies()
    {
        foreach (var enemy in _enemies)
            DestroyImmediate(enemy);

        _enemies.Clear();
    }
}
