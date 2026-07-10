using NUnit.Framework.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IdleHoMState : State
{
    private HeartOfStormEnemy _enemy;
    private NavMeshAgent _agent;
    private Coroutine _coroutine;
    private CollisionAttackChecker _leftCollisionAttackChecker;
    private CollisionAttackChecker _rightCollisionAttackChecker;
    private CollisionAttackChecker _headCollisionAttackChecker;
    private CollisionAttackChecker _areaCollisionAttackChecker;
    private Transform _headPoint;
    public IdleHoMState(HeartOfStormEnemy enemy, NavMeshAgent agent,
        CollisionAttackChecker leftCollisionAttackChecker,
        CollisionAttackChecker rightCollisionAttackChecker,
        CollisionAttackChecker headCollisionAttackChecker,
        CollisionAttackChecker areaCollisionAttackChecker,
        Transform headPoint
        )
    {
        _enemy = enemy;
        _agent = agent;
        _leftCollisionAttackChecker = leftCollisionAttackChecker;
        _rightCollisionAttackChecker = rightCollisionAttackChecker;
        _headCollisionAttackChecker = headCollisionAttackChecker;
        _areaCollisionAttackChecker = areaCollisionAttackChecker;
        _headPoint = headPoint;
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("В айдл");
        _agent.isStopped = true;
        _enemy.EnemyAnimator.SetBool("Idle", true);
        _coroutine = _enemy.StartCoroutine(waitUtilEndTimer());
        SpellCastData spellCastData = new SpellCastData()
        {
            Position = _headPoint.position,
            Caster = _enemy.gameObject,
            Direction = _headPoint.right,
            Target = _enemy.EnemyTarget.gameObject
        };
        _enemy.LastSpellCastData = spellCastData;
        //_enemy.SetCastIdleSpell(spellCastData);


    }


    public override void Exit()
    {
        base.Exit();

        if (_coroutine != null)
            _enemy.StopCoroutine(_coroutine);
        
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
        if (_leftCollisionAttackChecker.IsPlayerInside && _enemy.CanChooseNextAction)
        {
            _enemy.ChangeState<LeftAttackHoMState>();
        }
        else if (_headCollisionAttackChecker.IsPlayerInside && _enemy.CanChooseNextAction)
        {
            _enemy.ChangeState<JumpAttackHoMState>();
        }
        else if (_rightCollisionAttackChecker.IsPlayerInside && _enemy.CanChooseNextAction)
        {
            _enemy.ChangeState<RightAttackHoMState>();
        }
        else if(!_areaCollisionAttackChecker.IsPlayerInside && _enemy.CanChooseNextAction)
        {
            _enemy.ChangeState<TrampleHoMState>();
        }
        else if (_areaCollisionAttackChecker.IsPlayerInside && _enemy.CanChooseNextAction)
        {
            _enemy.ChangeState<MoveToPlayerHoMState>();
        }
        else
        {
            Debug.Log("Chase");
            _enemy.ChangeState<ChaseHoMState>();
        }
    }
}
