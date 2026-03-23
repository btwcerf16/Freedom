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
    public Action OnAllEnemiesClear;
    public void ActiveRoom(List<Enemy> enemies)
    {

        foreach (var e in enemies)
        {
            if (e == null)
                Debug.LogError("NULL ENEMY FOUND IN LIST");
        }

        foreach (Enemy enemy in enemies)
        {
            _agroedEnemies.Add(enemy);
            enemy.IsAgroed = true;
        }
        _attackersInWave = ((int)Mathf.Max(1, _agroedEnemies.Count * AttackPermission));
        AriseEnemies(enemies);
    }
    private void AriseEnemies(List<Enemy> enemies)
    {
        
        
        foreach (Enemy enemy in enemies)
        {
            
            if (_agroedEnemies.Contains(enemy) && ActivatedEnemies <= _attackersInWave)
            {
                ActivatedEnemies++;
                _agroedEnemies.Remove(enemy);
                
                _activeEnemies.Add(enemy);
                enemy.IsCombatActive = true;
                enemy.gameObject.SetActive(true);
                enemy.EnableAfterSpawn();
            }
            if (ActivatedEnemies >= _attackersInWave) {
                break;
            }
        }
    }
    public void ReArise()
    {
        ActivatedEnemies--;
        if(ActivatedEnemies == 0 && _agroedEnemies.Count > 0)
        {
            AriseEnemies(_agroedEnemies);
        }
        if (_agroedEnemies.Count == 0) 
        {
            OnAllEnemiesClear?.Invoke();
        }
    }
}