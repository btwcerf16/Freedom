using UnityEngine;

public class AttackEnemyState : State
{
    private Enemy _enemy;
    private StateMachine _stateMachine;
    private State _chaseState;
    private State _attackState;
    public AttackEnemyState(Enemy enemy, StateMachine stateMachine, State chaseState, State AttackState)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _chaseState = chaseState;
        _attackState = AttackState;
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
