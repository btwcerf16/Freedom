using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;

 
    public ChaseEnemyState(Enemy enemy, NavMeshAgent agent)
    {
        _enemy = enemy;
        _agent = agent;

    }

    public override void Enter()
    {
        Debug.Log("Начало погони");
        _enemy.EnemyAnimator.SetBool("Chase", true);
        _agent.isStopped = false;
    }

    public override void Exit()
    {
        base.Exit();
        _agent.isStopped = true;
        _enemy.EnemyAnimator.SetBool("Chase", false);
    }

    public override void Update()
    {
        if (!_agent.enabled || !_agent.isOnNavMesh)
            return;
        _agent.SetDestination(_enemy.EnemyTarget.position);
        if (_enemy.CanAttack())
        {
            _enemy.ChangeState<AttackEnemyState>();
        }
    }
}
