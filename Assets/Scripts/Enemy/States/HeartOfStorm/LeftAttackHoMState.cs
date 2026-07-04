using UnityEngine;
using UnityEngine.AI;

public class LeftAttackHoMState : State
{
    private HeartOfStormEnemy _enemy;
    private NavMeshAgent _agent;
    private Transform _legPoint;
    private Spell _spell;
    public LeftAttackHoMState(HeartOfStormEnemy enemy, NavMeshAgent agent, Transform legPoint, Spell spell)
    {
        _enemy = enemy;
        _agent = agent;
        _legPoint = legPoint;
        _spell = spell;
    }
    public override void Enter()
    {
        SpellCastData spellCastData = new SpellCastData()
        {
            Position = _legPoint.position,
            Caster = _enemy.gameObject,
            Direction = _legPoint.right,
            Target = _enemy.EnemyTarget.gameObject
        };
        _enemy.ActiveSpellCastData = spellCastData;
        _enemy.ActiveSpell = _spell;
        _enemy.EnemyAnimator.SetTrigger("LAttack");
        _agent.isStopped = true;

        
    }
    public override void Exit()
    {
        base.Exit();
        _enemy.EnemyAnimator.SetBool("Idle", true);
    }
}
