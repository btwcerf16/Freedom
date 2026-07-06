using UnityEngine;
using UnityEngine.AI;

public class RightAttackHoMState : State
{
    private HeartOfStormEnemy _enemy;
    private NavMeshAgent _agent;
    private Transform _legPoint;
    public RightAttackHoMState(HeartOfStormEnemy enemy, NavMeshAgent agent, Transform legPoint)
    {
        _enemy = enemy;
        _agent = agent;
        _legPoint = legPoint;

    }
    public override void Enter()
    {
        base.Enter();
        _agent.isStopped = true;
        SpellCastData spellCastData = new SpellCastData()
        {
            Position = _legPoint.position,
            Caster = _enemy.gameObject,
            Direction = _legPoint.right,
            Target = _enemy.EnemyTarget.gameObject
        };
        _enemy.LastSpellCastData = spellCastData;
        _enemy.SetCastRightLegSpell(spellCastData);
        _enemy.EnemyAnimator.SetTrigger("RAttack");

    }

    public override void Exit() 
    { 
        base.Exit();
        _agent.isStopped = true;
        _enemy.EnemyAnimator.SetBool("Idle", true);
    }
}
