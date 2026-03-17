using UnityEngine;

public class AriseEnemyState : State
{

    private Enemy _enemy;
    public AriseEnemyState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.EnemyAnimator.SetTrigger("Arise");

    }
    public override void Update()
    {
        base.Update();
        if (!_enemy.IsDisabled)
        {
            if (_enemy.CanAttack())
            {
                _enemy.ChangeState<AttackEnemyState>();
            }
            else
            {
                _enemy.ChangeState<ChaseEnemyState>();
            } 
        }

    }
    public override void Exit()
    {
        base.Exit();

    }
}
