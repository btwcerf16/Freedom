using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private StateMachine _stateMachine;
 
    public ChaseEnemyState(Enemy enemy, NavMeshAgent agent, StateMachine stateMachine)
    {
        _enemy = enemy;
        _agent = agent;
        _stateMachine = stateMachine;

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
        _agent.SetDestination(_enemy.EnemyTarget.position);

    }
}
