using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;

    [SerializeField] private List<Enemy> _roomEnemies = new();
        
    

    public void Initialize(List<Enemy> roomEnemies, EnemyController enemyController)
    {
        _roomEnemies = roomEnemies;
        
        
        if (enemyController == null)
        {
            Debug.LogError("ENEMY CONTROLLER IS NULL");
            return;
        }
        _enemyController = enemyController;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.CompareTag("Player") && _roomEnemies.Count >0)
        {

            Debug.Log("ѕытаюсь передать врагов в лист активных");
            _enemyController.ActivateRoom(_roomEnemies);
            Debug.Log("передал");
            _roomEnemies.Clear();
        }
    }

    public void SetSize(Vector2 size)
    {
        BoxCollider2D colider = GetComponent<BoxCollider2D>();
        colider.size = size;
    }
}
