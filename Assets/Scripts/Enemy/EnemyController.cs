using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<Enemy> _activeEnemies= new List<Enemy>();
    [SerializeField] private List<Enemy> _agroedEnemies= new List<Enemy>();
    [Range(0.0f, 1.0f)] public float AttackPermission;
    [SerializeField] private int _attackersInWave;
    public int activated;
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
        Debug.Log("Восстают");
        
        foreach (Enemy enemy in enemies)
        {
            activated++;
            if (_agroedEnemies.Contains(enemy) && activated <= _attackersInWave)
            {
                Debug.Log("Восстают1");
                _agroedEnemies.Remove(enemy);
                
                _activeEnemies.Add(enemy);
                enemy.IsCombatActive = true;
                enemy.gameObject.SetActive(true);
                enemy.EnableAfterSpawn();
            }
            if (activated >= _attackersInWave) {
                break;
            }
        }
    }
    public void ReArise()
    {
        activated--;
        if(activated == 0 && _agroedEnemies.Count > 0)
        {
            AriseEnemies(_agroedEnemies);
        } 
    }
}