using System;
using System.Collections;
using UnityEngine;

public class FrozenZombie : Enemy, IDisalable, IForceReceiver, IDamageable, IDisposable
{
    [SerializeField] private float _attackRadius = 1.4f;

    #region States



    #endregion


    private void Start()
    {
        EnemyStateMachine = new StateMachine();

        RegisterState(new DeathEnemyState(this, _agent));
        RegisterState(new AriseEnemyState(this));
        RegisterState(new ChaseEnemyState(this, _agent));
        RegisterState(new AttackEnemyState(this));
        RegisterState(new DisabledEnemyState(this, _agent));

    }
    public void Update()
    {

            EnemyStateMachine.CurrentState?.Update();

    }
    public override void EnableAfterSpawn()
    {
        base.EnableAfterSpawn();
        StartCoroutine(StartAriseState());
    }

    public override bool CanAttack()
    {
        float dist = Vector2.Distance(transform.position, EnemyTarget.position);
        return IsCombatActive && dist <= _attackDistance; 
    }

    public void Disable()
    {

    }
    IEnumerator StartAriseState()
    {

        yield return new WaitForSeconds(0.2f);
        ChangeState<AriseEnemyState>();
        yield return new WaitForSeconds(0.2f);
        _isArised = true;
    }

    public void ApplyForce(Vector2 direction, float force, float duration)
    {
        if (!IsDead)
            StartCoroutine(KnockbackCoroutine(direction, force, duration));
    }

    IEnumerator KnockbackCoroutine(Vector2 direction, float force, float duration)
    {


        float timer = 0;

        while (timer < duration)
        {
            transform.position += (Vector3)(direction * force * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
       // _animator.SetBool("Pushed", false);

        _agent.enabled = true;
    }

    public void GetDamage(float damage, bool isCrit)
    {
        if (damage >= EnemyStats.CurrentHealth.Value)
        {
            Debug.Log("œŒÃ≈–");
            IsDead = true;
            ChangeState<DeathEnemyState>();

        }
        //(Instantiate(_floatingDamage, damagePos, Quaternion.identity)).GetComponent<FloatingDamage>();
        Vector2 damagePos = new Vector2(transform.position.x + .5f, transform.position.y + 1.0f);
        FloatingDamage floatingDamage = PoolsController.Instance.DamageTextPool.GetObject();
        floatingDamage.transform.position = damagePos;

        floatingDamage.Damage = damage;
        if (isCrit)
            floatingDamage.Text.color = Color.darkRed;
        else
            floatingDamage.Text.color = Color.whiteSmoke;
        EnemyStats.CurrentHealth.Value -= damage;
    }
    public override void Death()
    {
        base.Death();
        _enemyController.ReArise();
    }
    private void DealDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Player"))
            {
                Debug.Log("œÓÔ‡Î");
                target.GetComponent<IDamageable>().GetDamage(EnemyStats.CurrentDamageAttack, false);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);

    }

    public void GetHeal(float heal)
    {
        EnemyStats.CurrentHealth.Value = Mathf.Clamp(EnemyStats.CurrentHealth.Value + heal, 0, EnemyStats.CurrentMaxHealth.Value);
    }

    public void Dispose()
    {
        Death();
    }
}
