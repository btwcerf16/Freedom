using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : Enemy, IForceReceiver, IDamageable
{
    [Header("Combat")]
 
    [SerializeField] private float _attackCooldown = 1.8f;
    [SerializeField] private float _attackRadius = 1.0f;

    private float cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (!_agent.isOnNavMesh) return;

        switch (_state)
        {
            case EEnemyState.Idle: Idle(); break;
            case EEnemyState.Arise: Arise(); break;
            case EEnemyState.WaitingTurn: WaitingTurn(); break;
            case EEnemyState.Chase: Chase(); break;
            case EEnemyState.Attack: Attack(); break;
            case EEnemyState.Death: Death(); break;
        }
        Flip();
    }

    public override bool CanAttack()
    {
        return IsAgroed && IsCombatActive && cooldownTimer <= 0.0f;
    }

    public override void Idle()
    {
        _animator.SetTrigger("Idle");
        _animator.SetBool("Chase", false);
        _agent.isStopped = true;
        float dist = Vector2.Distance(transform.position, _target.position);
        if(dist > _attackDistance)
            SetStateDelayed(EEnemyState.Chase, _attackCooldown);
        else
            SetStateDelayed(EEnemyState.Attack, _attackCooldown);
    }

    public override void WaitingTurn()
    {

        if (IsCombatActive)
            SetState(EEnemyState.Chase);    
    }

    public override void Chase()
    {
        Debug.Log("Чейзим");
        _animator.SetBool("Chase", true);
        _agent.isStopped = false;
        _agent.SetDestination(_target.position);
       

        float dist = Vector2.Distance(transform.position, _target.position);

        if (dist <= _attackDistance && CanAttack())
        {
            _animator.SetBool("Chase", false);
            _agent.isStopped = true;
            SetState(EEnemyState.Attack);
        }
    }

    public override void Attack()
    {
        float dist = Vector2.Distance(transform.position, _target.position);
        if (dist > _attackDistance)
        {
            SetState(EEnemyState.Chase);
            return;
        }

        _animator.SetTrigger("Attack");
        cooldownTimer = _attackCooldown;
        _agent.isStopped = true;
       SetState(EEnemyState.Idle);

    }
    public override void Arise()
    {
        SetState(EEnemyState.Chase);
    }
    public override void Flip()
    {
        if (_target.transform.position.x < transform.position.x)
        {
            transform.localScale = Vector2.one;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }
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

    public void ApplyForce(Vector2 direction, float force, float duration)
    {
        if(!IsDead)
            StartCoroutine(KnockbackCoroutine(direction, force, duration));
    }
    IEnumerator KnockbackCoroutine(Vector2 direction, float force, float duration)
    {
        _agent.enabled = false;
        _animator.SetBool("Pushed", true);
        _animator.SetBool("Chase", false);
        SetState(EEnemyState.Gag);
        float timer = 0;

        while (timer < duration)
        {
            transform.position += (Vector3)(direction * force * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        _animator.SetBool("Pushed", false);
        SetState(EEnemyState.Chase);
        _agent.enabled = true;
    }

    public void GetDamage(float damage, bool isCrit)
    {
        if (damage >= EnemyStats.CurrentHealth)
        {
            Debug.Log("ПОМЕР");
            IsDead = true;
            _animator.SetBool("IsDead", true);
            _agent.enabled = false;
            SetState(EEnemyState.Death);

        }
        Vector2 damagePos = new Vector2(transform.position.x + .5f, transform.position.y + 1.0f);
        FloatingDamage floatingDamage = (Instantiate(_floatingDamage, damagePos, Quaternion.identity)).GetComponent<FloatingDamage>();
        floatingDamage.Damage = damage; 
        if (isCrit)
            floatingDamage.Text.color = Color.darkRed;
        else
            floatingDamage.Text.color = Color.whiteSmoke;
        EnemyStats.CurrentHealth -= damage;
    }
    public override void Death()
    {
        _enemyController.ReArise();

      
        _animator.SetTrigger("Death");
       
        this.enabled = false;
        if(_isBoss)
            SceneTransition.SwitchScene("MainMenu");
    }
}