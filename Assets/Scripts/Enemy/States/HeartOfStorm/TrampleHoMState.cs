using UnityEngine;
using UnityEngine.AI;

public class TrampleHoMState : State
{
    private HeartOfStormEnemy _enemy;
    private NavMeshAgent _agent;
    private Transform _heartPoint;

    public TrampleHoMState(HeartOfStormEnemy enemy, NavMeshAgent agent, Transform heartPoint)
    {
        _enemy = enemy;
        _agent = agent;
        _heartPoint = heartPoint;
    }
    public override void Enter()
    {
        base.Enter();
        _agent.isStopped = true;
        SpellCastData spellCastData = new SpellCastData()
        {
            Position = _enemy.EnemyTarget.gameObject.transform.position,
            Caster = _enemy.gameObject,
            Direction = _heartPoint.right,
            Target = _enemy.EnemyTarget.gameObject
        };
        _enemy.LastSpellCastData = spellCastData;
        _enemy.SetCastTrampleSpell(spellCastData);
        _enemy.EnemyAnimator.SetTrigger("Trample");
    }
    public override void Exit()
    {
        base.Exit();
        _enemy.EnemyAnimator.SetBool("Idle", true);
        _agent.isStopped = false;
    }
}
