using UnityEngine;

public class AriseEnemyState : State
{
    private StateMachine _stateMachine;
    private State _attackState;
    private State _chaseState;
    private Enemy _enemy;
    public AriseEnemyState(StateMachine stateMachine, State attackState, State chaseState,Enemy enemy)
    {
        _stateMachine = stateMachine;
        _attackState = attackState;
        _chaseState = chaseState;
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.EnemyAnimator.SetTrigger("Arise");
        if (_enemy.CanAttack())
        {
            _stateMachine.ChangeState(_attackState);
            Debug.Log("Может бить - переходит в атаку");
        }
        else
        {
            _stateMachine.ChangeState(_chaseState);
            Debug.Log("Не может бить - переходит в погоню");
        }
    }

    public override void Exit()
    {
        base.Exit();

    }
}
