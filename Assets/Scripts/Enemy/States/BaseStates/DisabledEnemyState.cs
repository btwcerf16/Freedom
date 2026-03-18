using UnityEngine;
using UnityEngine.AI;

public class DisabledEnemyState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;

    public DisabledEnemyState(Enemy enemy, NavMeshAgent agent)
    {
        _enemy = enemy;
        _agent = agent;
    }

    public override void Enter()
    {
        base.Enter();
        _agent.enabled = false;
        _enemy.EnemyAnimator.SetBool("Disabled", true);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.EnemyAnimator.SetBool("Disabled", false);
        _agent.enabled = true;
    }

    public override void Update()
    {
        base.Update();
        if (!_enemy.IsDisabled)
        {
            _enemy.ChangeState<ChaseEnemyState>();
        }
    }
}
