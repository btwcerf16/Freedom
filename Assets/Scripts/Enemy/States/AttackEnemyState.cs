using UnityEngine;

public class AttackEnemyState : State
{
    private Enemy _enemy;

    public AttackEnemyState(Enemy enemy)
    {
        _enemy = enemy;
    }

    

    public override void Enter()
    {
        _enemy.EnemyAnimator.SetTrigger("Attack");
    }

    public override void Exit()
    {
        _enemy.EnemyAnimator.SetTrigger("Attack");
    }

    public override void Update()
    {
        if (!_enemy.CanAttack())
        {
            _enemy.ChangeState<ChaseEnemyState>();
            
        }

    }
}
