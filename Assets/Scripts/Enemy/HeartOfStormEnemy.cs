using System;
using UnityEngine;
[RequireComponent(typeof(EffectHandler), typeof(EffectDisplay), typeof(ActorStats))]
public class HeartOfStormEnemy : Enemy, IDisposable, IDamageable
{
    public Spell ActiveSpell;
    public SpellCastData ActiveSpellCastData;
    [SerializeField] private Transform _leftLegPoint;
    [SerializeField] private Transform _rightLegPoint;

    [SerializeField] private CollisionAttackChecker _leftAttackCollision;
    [SerializeField] private CollisionAttackChecker _rightAttackCollision;
    [SerializeField] private CollisionAttackChecker _headAttackCollision;

    private void Update()
    {
        if (EnemyStateMachine != null)
            EnemyStateMachine.Update();
        else Debug.Log("мер STATE MACHINE");
    }
    public void Initialize(EnemyController enemyController, Transform target, BoundsInt roomBounds)
    {
        base.Initialize(enemyController, target);
        EnemyStateMachine = new StateMachine();

        RegisterState(new ChaseHoMState(this, _agent, roomBounds));
        RegisterState(new DeathHoMState());
        RegisterState(new IdleHoMState(this, _agent, _leftAttackCollision, _rightAttackCollision, _headAttackCollision));
        RegisterState(new JumpAttackHoMState(this));

        RegisterState(new RightAttackHoMState(this));
        RegisterState(new RushHoMState());
        if (_spellHolder.Spells != null)
            RegisterState(new LeftAttackHoMState(this, _agent, _leftLegPoint, _spellHolder.Spells[0]));
        _agent.enabled = true;
        ChangeState<ChaseHoMState>();
    }
    public override void Death()
    {
        base.Death();
    }
    public void CastSpell()
    {

        ActiveSpell.Cast(ActiveSpellCastData);
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
   
        Vector2 damagePos = new Vector2(transform.position.x + .5f + UnityEngine.Random.value*2, transform.position.y + 1.0f+ UnityEngine.Random.value);
        FloatingDamage floatingDamage = PoolsController.Instance.DamageTextPool.GetObject();
        floatingDamage.transform.position = damagePos;

        floatingDamage.Damage = damage;
        if (isCrit)
            floatingDamage.Text.color = Color.darkRed;
        else
            floatingDamage.Text.color = Color.whiteSmoke;
        EnemyStats.CurrentHealth.Value -= damage;
        
    }

    public void GetHeal(float heal)
    {
        EnemyStats.CurrentHealth.Value = Mathf.Clamp(EnemyStats.CurrentHealth.Value + heal, 0, EnemyStats.MaxHealth.CurrentValue);
    }
    #endregion
}

