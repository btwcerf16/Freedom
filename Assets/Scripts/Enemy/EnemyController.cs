using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<Enemy> _activeEnemies= new List<Enemy>();
    [SerializeField] private List<Enemy> _agroedEnemies= new List<Enemy>();
    [Range(0.0f, 1.0f)] public float AttackPermission;
    [SerializeField] private int _attackersInWave;
    public int ActivatedEnemies;
    public int EnemiesCount;
    public Action OnAllEnemiesClear;

    private List<Enemy> _currentRoom;
    public void ActiveRoom(List<Enemy> enemies)
    {
        
        if (_currentRoom != null)
        {
            Debug.Log("Сначала зачисти текущую комнату");
            return;
        }

        _currentRoom = enemies;

        _agroedEnemies.Clear();
        _activeEnemies.Clear();
        ActivatedEnemies = 0;

        foreach (Enemy enemy in enemies)
        {
            if (enemy == null) continue;

            _agroedEnemies.Add(enemy);
            enemy.IsAgroed = true;
        }

        _attackersInWave = Mathf.Max(1, Mathf.RoundToInt(_agroedEnemies.Count * AttackPermission));

        AriseEnemies();
    }
    private void AriseEnemies()
    {
        int toSpawn = Mathf.Min(_attackersInWave, _agroedEnemies.Count);

        for (int i = 0; i < toSpawn; i++)
        {
            var enemy = _agroedEnemies[0]; 

            if (enemy == null)
            {
                _agroedEnemies.RemoveAt(0);
                i--;
                continue;
            }

            Debug.Log("Заспавнил врага");

            ActivatedEnemies++;

            _agroedEnemies.RemoveAt(0);
            _activeEnemies.Add(enemy);

            enemy.IsCombatActive = true;
            enemy.gameObject.SetActive(true);
            enemy.EnableAfterSpawn();
        }
    }
    public void ReArise()
    {
        ActivatedEnemies--;
        EnemiesCount--;

        if (EnemiesCount <= 0)
        {
            EnemiesCount = 0;
            Debug.Log("ВСЕХ УБИЛ");
            OnAllEnemiesClear?.Invoke();
        }

       
        if (ActivatedEnemies <= 0 && _agroedEnemies.Count > 0)
        {
            Debug.Log("НОВАЯ ВОЛНА");
            AriseEnemies();
        }

        if (ActivatedEnemies <= 0 && _agroedEnemies.Count == 0)
        {
            Debug.Log("Комната зачищена");

            _currentRoom = null; 
        }
    }
}