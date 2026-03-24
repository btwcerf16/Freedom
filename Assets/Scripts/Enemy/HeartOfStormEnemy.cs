using System;
using UnityEngine;
[RequireComponent(typeof(EffectHandler), typeof(EffectDisplay), typeof(ActorStats))]
public class HeartOfStormEnemy : Enemy, IDisposable, IDamageable
{


    private void Start()
    {
        EnemyStateMachine = new StateMachine();

        RegisterState(new ChaseHoMState(this, _agent));
        RegisterState(new DeathHoMState());
        RegisterState(new IdleHoMState());
        RegisterState(new LeftAttackHoMState());
        RegisterState(new RightAttackHoMState());
        RegisterState(new RushHoMState());

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

