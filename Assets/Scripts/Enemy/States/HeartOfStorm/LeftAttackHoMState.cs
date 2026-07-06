using UnityEngine;
using UnityEngine.AI;

public class LeftAttackHoMState : State
{
    private HeartOfStormEnemy _enemy;
    private NavMeshAgent _agent;
    private Transform _legPoint;
    private Spell _spell;
    public LeftAttackHoMState(HeartOfStormEnemy enemy, NavMeshAgent agent, Transform legPoint)
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
        _enemy.SetCastLeftLegSpell(spellCastData);
        _enemy.EnemyAnimator.SetTrigger("LAttack");
       

        
    }
    public override void Exit()
    {
        base.Exit();
        _agent.isStopped = false;
        _enemy.EnemyAnimator.SetBool("Idle", true);
    }
}
