using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<Enemy> _attackQueue = new List<Enemy>();
    [SerializeField] private List<Enemy> _enemiesIsAgroed = new List<Enemy>();//все враги в 1 комнате
    [Range(0.0f, 1)] public float AttackPressure;

    private void Update()
    {
        CombatLogic();
    }
    private void CombatLogic()
    {
        if (_enemiesIsAgroed.Count == 0) return;

        int maxAttackers = Mathf.CeilToInt(_enemiesIsAgroed.Count * AttackPressure);

        // текущие атакующие
        _attackQueue.RemoveAll(e => e == null || !e.IsAgroed);

        if (_attackQueue.Count < maxAttackers)
        {
            TryAddAttackers(maxAttackers - _attackQueue.Count);
        }
        if(_attackQueue.Count > maxAttackers)
        {
            DiscardAttacker(maxAttackers);
        }
    }

    private void TryAddAttackers(int count)
    {
        foreach (var enemy in _enemiesIsAgroed)
        {
            if (_attackQueue.Contains(enemy)) continue;
            if (!enemy.CanAttack()) continue;

            _attackQueue.Add(enemy);
            enemy.StartAttackPermission();

            count--;
            if (count <= 0) break;
        }
    }
    private void DiscardAttacker(int maxAttackers)
    {
        foreach(var enemy in _attackQueue)
        {
            enemy.EndAttackPermission();
        }
        _attackQueue.Clear();
        TryAddAttackers(maxAttackers);
    }
    public int GetEnemyIndex(Enemy enemy)
    {
        return _enemiesIsAgroed.IndexOf(enemy);
    }

    public int GetEnemiesCount()
    {
        return _enemiesIsAgroed.Count;
    }
    public void RegisterEnemy(Enemy enemy)
    {
        _enemiesIsAgroed.Add(enemy);
        enemy.SetState(EEnemyState.WaitingTurn);
    }
    public void UnregisterEnemy(Enemy enemy)
    {
        _enemiesIsAgroed.Remove(enemy);
    }
}
