using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float attackRange = 1.2f;

    protected override void Act(float distance)
    {
        if (!agent.isOnNavMesh) return;

        if (distance > attackRange)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.ResetPath();

            if (canAttack && _enemyController.CanAttack(this))
            {
                Attack();
                StartCoroutine(AttackCooldownRoutine());
            }
        }
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log($"{name} наносит удар игроку!");
        // TODO: урон игроку
        _enemyController.EnemyDoneAttacking(this);
    }

}
