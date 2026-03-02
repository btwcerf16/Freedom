using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    private Enemy activeEnemy;

    public void RegisterEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        enemy.SetManager(this);
    }

    public bool CanAttack(Enemy enemy)
    {
        if (activeEnemy == null || activeEnemy == enemy)
        {
            activeEnemy = enemy;
            return true;
        }
        return false;
    }

    public void EnemyDoneAttacking(Enemy enemy)
    {
        if (activeEnemy == enemy)
            activeEnemy = null;
    }
    public void ClearRegisteredEnemy()
    {
        enemies.Clear();
    }
}
