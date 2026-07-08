using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
[RequireComponent(typeof(EffectHandler), typeof(EffectDisplay), typeof(ActorStats))]

public class HeartOfStormEnemy : Enemy, IDisposable, IDamageable
{
    public Spell LastSpell;
    public SpellCastData LastSpellCastData;
    [SerializeField] private Transform _leftLegPoint;
    [SerializeField] private Transform _rightLegPoint;
    [SerializeField] private Transform _headPoint;

    [SerializeField] private CollisionAttackChecker _leftAttackCollision;
    [SerializeField] private CollisionAttackChecker _rightAttackCollision;
    [SerializeField] private CollisionAttackChecker _headAttackCollision;

    [SerializeField] private float _attackRadius = 6.0f;

    [SerializeField] private float _knockbackSrength = 20.0f;
    [SerializeField] private float _knockbackTime = 0.3f;

    [SerializeField] private Coroutine _endGameCorutine;
    public bool CanChooseNextAction;
    private void Update()
    {
        if (EnemyStateMachine != null)
            EnemyStateMachine.Update();
        else Debug.Log("НЕТ STATE MACHINE");
    }
    public void Initialize(EnemyController enemyController, Transform target, BoundsInt roomBounds)
    {
        base.Initialize(enemyController, target);
        EnemyStateMachine = new StateMachine();
        _spellHolder = GetComponent<SpellHolder>();
        RegisterState(new ChaseHoMState(this, _agent, roomBounds));
        RegisterState(new DeathHoMState());
        RegisterState(new IdleHoMState(this, _agent, _leftAttackCollision, _rightAttackCollision, _headAttackCollision, _headPoint));
        RegisterState(new JumpAttackHoMState(this, _agent, _headPoint));
        RegisterState(new RightAttackHoMState(this, _agent, _rightLegPoint));
        RegisterState(new RushHoMState());
        RegisterState(new LeftAttackHoMState(this, _agent, _leftLegPoint));
        _agent.enabled = true;
        ChangeState<ChaseHoMState>();
    }
    public override void Death()
    {
        base.Death();
        _agent.enabled = false;
        if(_endGameCorutine == null)
            _endGameCorutine = StartCoroutine(EndGame());
        //gameObject.SetActive(false);
        
    }
    public void ResumeBehavior()
    {
        CanChooseNextAction = false;
        ChangeState<IdleHoMState>();
    }
    private void LeftTrumple()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(_leftLegPoint.position, _attackRadius);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Player"))
            {
                Debug.Log("Попал");
                target.GetComponent<IDamageable>()?.GetDamage(EnemyStats.AttackDamage.CurrentValue, true);
                Knockback(target);
                target.GetComponent<PlayableActor>()?.PlayerCinemachineCamera.GetComponent<CinemachineShake>().ShakeCamera(2.5f, 0.5f);
            }
            
        }
    }
    private void RightTrumple()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(_rightLegPoint.position, _attackRadius);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Player"))
            {
                Debug.Log("Попал");
                target.GetComponent<IDamageable>()?.GetDamage(EnemyStats.AttackDamage.CurrentValue, true);
                Knockback(target);
                target.GetComponent<PlayableActor>()?.PlayerCinemachineCamera.GetComponent<CinemachineShake>().ShakeCamera(2.5f, 0.5f);
            }
        }
    }
    private void JumpHit()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(_headPoint.position, _attackRadius);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Player"))
            {
                Debug.Log("Попал");
                target.GetComponent<IDamageable>()?.GetDamage(EnemyStats.AttackDamage.CurrentValue * 1.3f, true);
                if (target.gameObject.activeSelf)
                {
                    Knockback(target);
                    target.GetComponent<PlayableActor>()?.PlayerCinemachineCamera.GetComponent<CinemachineShake>().ShakeCamera(2.5f, 0.5f);
                }
                    
                
                
            }

        }
    }
    private void Knockback(Collider2D target)
    {
        IForceReceiver forceReceiver = target.GetComponent<IForceReceiver>();

        if (forceReceiver != null)
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            forceReceiver.ApplyForce(dir, _knockbackSrength, _knockbackTime);
        }
    }

    #region Spells
    public void ExecuteAnimationSpell()
    {
        if (LastSpell == null)
        {
            Debug.LogError("LastSpell == null");
            return;
        }
        LastSpell.Cast(LastSpellCastData);
    }
    public void SetCastIdleSpell(SpellCastData _spellCastData)
    {
        int index = 1;
        LastSpell = _spellHolder.Spells[index];
        LastSpellCastData = _spellCastData;
        LastSpell.Cast(LastSpellCastData);
    }
    public void SetCastJumpSpell(SpellCastData _spellCastData)
    {
        int index = 0;
        LastSpell = _spellHolder.Spells[index];
        LastSpellCastData = _spellCastData;
    }
    public void SetCastLeftLegSpell(SpellCastData _spellCastData)
    {
        int index = 0;
        LastSpell = _spellHolder.Spells[index];
        LastSpellCastData = _spellCastData;
    }
    public void SetCastRightLegSpell(SpellCastData _spellCastData)
    {
        int index = 0;
        LastSpell = _spellHolder.Spells[index];
        LastSpellCastData = _spellCastData;
    }
    #endregion

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
   
        Vector2 damagePos = new Vector2(transform.position.x - .5f - UnityEngine.Random.value*2, transform.position.y - 1.0f- UnityEngine.Random.value);
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
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2.0f);
        SceneTransition.SwitchScene("MainMenu");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_leftLegPoint.position, _attackRadius);
        Gizmos.DrawWireSphere(_rightLegPoint.position, _attackRadius);
        Gizmos.DrawWireSphere(_headPoint.position, _attackRadius);
    }
}

