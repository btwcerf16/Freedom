using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySummoner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnedEnemies = new();
    [SerializeField] private Transform _player;
    [SerializeField] private EnemyController _enemyController;

    public List<Enemy> SummonEnemies(DungeonSO data, HashSet<Vector2Int> room, ETypeRoom typeRoom)
    {
        List<Enemy> roomEnemies = new();

        Vector2Int center = ProceduralGenerationAlgorithm.GetRoomCenter(room);
        Vector3 spawnPos = new Vector3(center.x + 0.5f, center.y + 0.5f, 0);

        if (typeRoom == ETypeRoom.BossRoom)
        {
            GameObject obj = Instantiate(
                data.Bosses[Random.Range(0, data.Bosses.Count)],
                spawnPos,
                Quaternion.identity);

            Enemy enemy = InitializeEnemy(obj);
            obj.SetActive(false);
            if (enemy != null) roomEnemies.Add(enemy);

            _spawnedEnemies.Add(obj);
            return roomEnemies;
        }

        int enemyCount = Random.Range(data.minEnemiesInRoom, data.maxEnemiesInRoom);
        
        foreach (var pos in room.OrderBy(_ => Guid.NewGuid()).Take(enemyCount))
        {
            GameObject obj = Instantiate(
                data.EnemyList[Random.Range(0, data.EnemyList.Count)],
                new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0),
                Quaternion.identity);

            Enemy enemy = InitializeEnemy(obj);
            obj.SetActive(false);
            if (enemy != null) roomEnemies.Add(enemy);

            _spawnedEnemies.Add(obj);
           
        }
        _enemyController.EnemiesCount += roomEnemies.Count;
        return roomEnemies;
    }

    private Enemy InitializeEnemy(GameObject enemyObj)
    {
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogWarning($"У {enemyObj.name} нет Enemy");
            return null;
        }

        enemy.Initialize(_enemyController, _player);
        return enemy;
    }

    public void ClearAllEnemies()
    {
        foreach (var enemy in _spawnedEnemies)
            DestroyImmediate(enemy);

        _spawnedEnemies.Clear();
    }

    
}