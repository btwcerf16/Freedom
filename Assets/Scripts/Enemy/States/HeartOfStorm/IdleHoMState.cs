using NUnit.Framework.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IdleHoMState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Coroutine _coroutine;
    private CollisionAttackChecker _leftCollisionAttackChecker;
    private CollisionAttackChecker _rightCollisionAttackChecker;
    private CollisionAttackChecker _headCollisionAttackChecker;
    public IdleHoMState(Enemy enemy, NavMeshAgent agent, 
        CollisionAttackChecker leftCollisionAttackChecker,
        CollisionAttackChecker rightCollisionAttackChecker,
        CollisionAttackChecker headCollisionAttackChecker
        )
    {
        _enemy = enemy;
        _agent = agent;
        _leftCollisionAttackChecker = leftCollisionAttackChecker;
        _rightCollisionAttackChecker = rightCollisionAttackChecker;
        _headCollisionAttackChecker = headCollisionAttackChecker;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("В айдл");
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
        Debug.Log("Ожидание");
        yield return new WaitForSeconds(1.0f);
        if (_leftCollisionAttackChecker.IsPlayerInside)
        {
            _enemy.ChangeState<LeftAttackHoMState>();
        }
        else if (_rightCollisionAttackChecker.IsPlayerInside)
        {
            _enemy.ChangeState<RightAttackHoMState>();
        }
        else if (_headCollisionAttackChecker.IsPlayerInside)
        {
            _enemy.ChangeState<JumpAttackHoMState>();
        }
        else
        {
            _enemy.ChangeState<ChaseHoMState>();
        }
    }
}
