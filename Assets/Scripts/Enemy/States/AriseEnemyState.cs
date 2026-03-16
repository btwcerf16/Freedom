using UnityEngine;

public class AriseEnemyState : State
{
    private StateMachine _stateMachine;
    private Enemy _enemy;
    private State _attackState;
    private State _chaseState;
    public AriseEnemyState(StateMachine stateMachine, Enemy enemy, State attackState, State chaseState)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
        _attackState = attackState;
        _chaseState = chaseState;

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
