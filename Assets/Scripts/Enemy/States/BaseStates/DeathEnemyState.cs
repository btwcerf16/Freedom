using UnityEngine;
using UnityEngine.AI;


public class DeathEnemyState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    public DeathEnemyState(Enemy enemy, NavMeshAgent agent)
    {
        _enemy = enemy;
        _agent = agent;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.EnemyAnimator.SetBool("IsDead", true);
        _agent.enabled = false;
        _enemy.Death();
        Debug.Log("Вызвал смерть");
        _enemy.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
