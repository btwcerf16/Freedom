using UnityEngine;
using UnityEngine.AI;

public class JumpAttackHoMState : State
{
    private HeartOfStormEnemy _enemy;
    private NavMeshAgent _agent;
    private Transform _headPoint;
    
    public JumpAttackHoMState(HeartOfStormEnemy enemy, NavMeshAgent agent, Transform headPoint)
    {
        _enemy = enemy;
        _agent = agent;
        _headPoint = headPoint;

    }
    public override void Enter()
    {
        base.Enter();
        
        _agent.isStopped = true;
        SpellCastData spellCastData = new SpellCastData()
        {
            Position = _headPoint.position,
            Caster = _enemy.gameObject,
            Direction = _headPoint.right,
            Target = _enemy.EnemyTarget.gameObject
        };
        _enemy.LastSpellCastData = spellCastData;
        _enemy.SetCastJumpSpell(spellCastData);
        _enemy.EnemyAnimator.SetTrigger("JAttack");
    }
    public override void Exit() 
    {
        base.Exit();
        _enemy.EnemyAnimator.SetBool("Idle", true);
        _agent.isStopped = false;
        

    }
}
