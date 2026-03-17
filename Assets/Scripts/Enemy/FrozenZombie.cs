using System.Collections;
using UnityEngine;

public class FrozenZombie : Enemy, IDisalable, IForceReceiver, IDamageable
{
    [SerializeField] private float _attackRadius = 1.4f;

    #region States



    #endregion


    private void Start()
    {
        EnemyStateMachine = new StateMachine();

        RegisterState(new AriseEnemyState(this));
        RegisterState(new ChaseEnemyState(this, _agent));
        RegisterState(new AttackEnemyState(this));

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
        _agent.enabled = false;
        //_animator.SetBool("Pushed", true);
        //_animator.SetBool("Chase", false);
        //SetState(EEnemyState.Gag);
        float timer = 0;

        while (timer < duration)
        {
            transform.position += (Vector3)(direction * force * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
       // _animator.SetBool("Pushed", false);
        //SetState(EEnemyState.Chase);
        _agent.enabled = true;
    }

    public void GetDamage(float damage, bool isCrit)
    {
        if (damage >= EnemyStats.CurrentHealth.Value)
        {
            Debug.Log("ПОМЕР");
            IsDead = true;
            MeleeDeath();

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
    public void MeleeDeath()
    {

        //_animator.SetBool("IsDead", true);
        _agent.enabled = false;
        Death();
        Debug.Log("Вызвал смерть");

        //_animator.SetTrigger("Death");

        enabled = false;

    }
    private void DealDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Player"))
            {
                Debug.Log("Попал");
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
}
