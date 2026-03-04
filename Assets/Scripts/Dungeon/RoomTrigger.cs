using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;

    private List<Enemy> _roomEnemies;
    private bool _activated;

    public void Initialize(List<Enemy> roomEnemies, EnemyController enemyController)
    {
        _roomEnemies = roomEnemies;
        _enemyController = enemyController;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_activated) return;

        if (other.CompareTag("Player"))
        {
            _activated = true;

            _enemyController.ActiveRoom(_roomEnemies);
            
        }
    }

    public void SetSize(Vector2 size)
    {
        BoxCollider2D colider = GetComponent<BoxCollider2D>();
        colider.size = size;
    }
}
