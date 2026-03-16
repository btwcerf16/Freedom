using UnityEngine;

public class AttackEnemyState : State
{
    private Enemy _enemy;
    private StateMachine _stateMachine;
    private State _attackState;
    private State _chaseState;
    public AttackEnemyState(Enemy enemy, StateMachine stateMachine, State attackState, State chaseState)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _attackState = attackState;
        _chaseState = chaseState;   
    }

    public override void Enter()
    {
        _enemy.EnemyAnimator.SetBool("Attack", true);
        Debug.Log("Старт атаки");
    }

    public override void Exit()
    {
        _enemy.EnemyAnimator.SetBool("Attack", false);
        base.Exit();
    }

    public override void Update()
    {
        Debug.Log("Обнова атаки");
        if (_enemy.CanAttack())
        {
            Debug.Log("Продолжаю бить");
            _stateMachine.ChangeState(_attackState);
        }
        else {
            Debug.Log("Чейзю");
            _stateMachine.ChangeState(_chaseState);
        }
    }
}
