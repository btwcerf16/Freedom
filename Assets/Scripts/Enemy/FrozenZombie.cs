using UnityEngine;

public class FrozenZombie : Enemy, IDisalable
{

    #region States

    private AriseEnemyState _ariseState;
    private ChaseEnemyState _chaseState;
    private AttackEnemyState _attackState;

    #endregion


    private void Awake()
    {
        EnemyStateMachine = new StateMachine();
        _attackState = new AttackEnemyState(this, EnemyStateMachine, _chaseState, _attackState);
        _chaseState = new ChaseEnemyState(this, _agent, EnemyStateMachine, _attackState);
        

        _ariseState = new AriseEnemyState(EnemyStateMachine, _attackState, _chaseState, this);
    }
    public void Update()
    {
        Debug.Log(EnemyStateMachine.CurrentState?.ToString());
        EnemyStateMachine.CurrentState?.Update();
    }
    public override void EnableAfterSpawn()
    {
        base.EnableAfterSpawn();
        EnemyStateMachine.Initialize(_ariseState);
    }

    public override bool CanAttack()
    {
        float dist = Vector2.Distance(transform.position, EnemyTarget.position);
        return IsCombatActive && dist <= _attackDistance; 
    }

    public void Disable()
    {
        
    }
}
