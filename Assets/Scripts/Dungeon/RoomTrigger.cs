using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private List<Enemy> _enemies;
    private EnemyController _enemyController;
    private bool _activated;

    public void Initialize(List<Enemy> enemies, EnemyController controller)
    {
        _enemies = enemies;
        _enemyController = controller;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_activated) return;
        if (!other.CompareTag("Player")) return;

        _activated = true;

        foreach (var enemy in _enemies)
        {
            if (enemy == null) continue;

            enemy.IsAgroed = true;
            _enemyController.RegisterEnemy(enemy);
        }
    }
    public void SetSize(Vector2 size)
    {
        BoxCollider2D colider = GetComponent<BoxCollider2D>();
        colider.size = size;
    }
}
