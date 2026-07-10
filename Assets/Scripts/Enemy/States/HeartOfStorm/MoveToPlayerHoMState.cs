using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayerHoMState : State
{
    private HeartOfStormEnemy _enemy;
    private NavMeshAgent _agent;

    private CollisionAttackChecker _leftCollisionAttackChecker;
    private CollisionAttackChecker _rightCollisionAttackChecker;
    private CollisionAttackChecker _headCollisionAttackChecker;
    private CollisionAttackChecker _areaCollisionAttackChecker;

    public MoveToPlayerHoMState(HeartOfStormEnemy enemy, NavMeshAgent agent,
        CollisionAttackChecker leftCollisionAttackChecker,
        CollisionAttackChecker rightCollisionAttackChecker,
        CollisionAttackChecker headCollisionAttackChecker,
        CollisionAttackChecker areaCollisionAttackChecker)
    {
        _enemy = enemy;
        _agent = agent;
        _leftCollisionAttackChecker = leftCollisionAttackChecker;
        _rightCollisionAttackChecker = rightCollisionAttackChecker;
        _headCollisionAttackChecker = headCollisionAttackChecker;
        _areaCollisionAttackChecker = areaCollisionAttackChecker;
    }
    public override void Enter()
    {
        Debug.Log("œŒÿ≈À   »√–Œ ”");
        _agent.isStopped = false;
        _agent.SetDestination(_enemy.EnemyTarget.position);
        _enemy.EnemyAnimator.SetBool("Chase", true);
    }

    public override void Update()
    {
        if (ReachedDestination())
        {
            
            _enemy.ChangeState<IdleHoMState>();
        }
    }
    public override void Exit()
    {
        base.Exit();
        _enemy.EnemyAnimator.SetBool("Chase", false);
    }
    private bool ReachedDestination()
    {
        if (_agent.pathPending) return false;
        if (!_agent.hasPath) return false;
        if (_agent.remainingDistance > _agent.stoppingDistance) return false;

        return true;
    }

}
