using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ActorStats EnemyActorStats;
    protected Rigidbody2D _rigidBody2D;
    protected BoxCollider2D _boxCollider;


    private void InitializeEnemy()
    {
        EnemyActorStats = GetComponent<ActorStats>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
}
