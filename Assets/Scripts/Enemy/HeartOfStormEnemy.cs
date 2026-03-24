using System;
using UnityEngine;
[RequireComponent(typeof(EffectHandler), typeof(EffectDisplay), typeof(ActorStats))]
public class HeartOfStormEnemy : Enemy, IDisposable, IDamageable
{
  

    private void Update()
    {
        if (EnemyStateMachine != null)
            EnemyStateMachine.Update();
        else Debug.Log("мер");
    }
    public void Initialize(EnemyController enemyController, Transform target, BoundsInt roomBounds)
    {
        base.Initialize(enemyController, target);
        EnemyStateMachine = new StateMachine();

        RegisterState(new ChaseHoMState(this, _agent, roomBounds));
        RegisterState(new DeathHoMState());
        RegisterState(new IdleHoMState(this, _agent));
        RegisterState(new LeftAttackHoMState());
        RegisterState(new RightAttackHoMState());
        RegisterState(new RushHoMState());
        _agent.enabled = true;
        ChangeState<ChaseHoMState>();
    }
    public override void Death()
    {
        base.Death();
    }

    #region Interfaces
    public void Dispose()
    {
        Death();
    }
    
    public void GetDamage(float damage, bool isCrit)
    {
        if(damage >= EnemyStats.CurrentHealth.Value)
        {
            Dispose();
        }
        EnemyStats.CurrentHealth.Value-= damage;
    }

    public void GetHeal(float heal)
    {
        EnemyStats.CurrentHealth.Value = Mathf.Clamp(EnemyStats.CurrentHealth.Value + heal, 0, EnemyStats.CurrentMaxHealth.Value);
    }
    #endregion
}

