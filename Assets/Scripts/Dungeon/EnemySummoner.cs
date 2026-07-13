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

        List<EnemyData> enemiesToSpawn = Calculate(data);

        var positions = room
            .OrderBy(_ => Guid.NewGuid())
            .Take(enemiesToSpawn.Count)
            .ToList();

        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            GameObject obj = Instantiate(
                enemiesToSpawn[i].Enemy,
                new Vector3(positions[i].x + 0.5f, positions[i].y + 0.5f, 0),
                Quaternion.identity);

            Enemy enemy = InitializeEnemy(obj);
            obj.SetActive(false);

            if (enemy != null)
                roomEnemies.Add(enemy);

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
        _enemyController.EnemiesCount = 0;
    }
    public List<EnemyData> Calculate(DungeonSO data)
    {
        List<EnemyData> result = new();

        int budget = Random.Range(data.MinRoomBudget, data.MaxRoomBudget + 1);

        while (true)
        {
            List<(EnemyData enemy, float weight)> candidates = new();

            float totalWeight = 0;

            foreach (EnemyData enemy in data.EnemyList)
            {
                if (enemy.EnemyCost > budget)
                    continue;

                float weight = budget - enemy.EnemyCost + 1;

                candidates.Add((enemy, weight));
                totalWeight += weight;
            }

            if (candidates.Count == 0)
                break;

            float roll = Random.Range(0f, totalWeight);

            foreach (var candidate in candidates)
            {
                roll -= candidate.weight;

                if (roll <= 0)
                {
                    result.Add(candidate.enemy);
                    budget -= candidate.enemy.EnemyCost;
                    break;
                }
            }
        }

        return result;
    }

}