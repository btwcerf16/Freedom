using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IdleHoMState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Coroutine _coroutine;
    public IdleHoMState(Enemy enemy, NavMeshAgent agent)
    {
        _enemy = enemy;
        _agent = agent;
    }

    public override void Enter()
    {
        base.Enter();
        _agent.isStopped = true;
        _enemy.EnemyAnimator.SetBool("Idle", true);
        _coroutine = _enemy.StartCoroutine(waitUtilEndTimer());
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.StopCoroutine(waitUtilEndTimer());
        _enemy.EnemyAnimator.SetBool("Idle", false);
    }

    public override void Update()
    {
        base.Update();
    }
    IEnumerator waitUtilEndTimer()
    {
        yield return new WaitForSeconds(0.5f);
        _enemy.ChangeState<ChaseHoMState>();
    }
}
