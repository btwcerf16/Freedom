using UnityEngine;
using UnityEngine.AI;


public class ChaseHoMState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;


    public ChaseHoMState(Enemy enemy, NavMeshAgent agent)
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

}


