using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;

    private List<Enemy> _roomEnemies;
 

    public void Initialize(List<Enemy> roomEnemies, EnemyController enemyController)
    {
        _roomEnemies = roomEnemies;
        _enemyController = enemyController;
        if (_enemyController == null)
        {
            Debug.LogError("ENEMY CONTROLLER IS NULL");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.CompareTag("Player") )
        {
            
            
            _enemyController.ActiveRoom(_roomEnemies);
            
        }
    }

    public void SetSize(Vector2 size)
    {
        BoxCollider2D colider = GetComponent<BoxCollider2D>();
        colider.size = size;
    }
}
