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
    public void SummonEnemies(DungeonSO data,HashSet<Vector2Int> room,ETypeRoom typeRoom)
    {

        Vector2Int center = ProceduralGenerationAlgorithm.GetRoomCenter(room);

        Vector3 spawnPos = new Vector3(center.x + 0.5f, center.y + 0.5f, 0);

        if (typeRoom == ETypeRoom.BossRoom)
        {
            GameObject obj = Instantiate(
                data.Bosses[Random.Range(0, data.Bosses.Count)],
                spawnPos,
                Quaternion.identity);

            InitializeEnemy(obj);
            _spawnedEnemies.Add(obj);
            return;
        }

        int enemyCount = Random.Range(data.minEnemiesInRoom, data.maxEnemiesInRoom);

        foreach (var pos in room.OrderBy(_ => Guid.NewGuid()).Take(enemyCount))
        {
            GameObject obj = Instantiate(
                data.EnemyList[Random.Range(0, data.EnemyList.Count)],
                new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0),
                Quaternion.identity);

            InitializeEnemy(obj);
            _spawnedEnemies.Add(obj);
            
        }
    }

    private void InitializeEnemy(GameObject enemyObj)
    {
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogWarning($"У {enemyObj.name} нет компонента Enemy");
            return;
        }

        enemy.SetTarget(_player);
        enemy.TriggerAggro();        
        _enemyController.RegisterEnemy(enemy);
    }

    public void ClearAllEnemies()
    {
        foreach (var enemy in _spawnedEnemies)
            DestroyImmediate(enemy);

        _spawnedEnemies.Clear();
        _enemyController.ClearRegisteredEnemy();
    }
}