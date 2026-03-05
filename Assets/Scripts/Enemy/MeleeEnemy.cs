using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Combat")]
    [SerializeField] private float attackDistance = 1.4f;
    [SerializeField] private float attackCooldown = 1.8f;


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
        }
    }

    public override bool CanAttack()
    {
        return IsAgroed && IsCombatActive && cooldownTimer <= 0.0f;
    }

    public override void Idle()
    {
        _animator.SetTrigger("Idle");
        _agent.isStopped = true;
        float dist = Vector3.Distance(transform.position, _target.position);
        if (dist <= attackDistance && CanAttack())
        {
            SetState(EEnemyState.Attack);
        }
        else { SetStateDelayed(EEnemyState.Chase, attackCooldown); }
          
    }

    public override void WaitingTurn()
    {

        if (IsCombatActive)
            SetState(EEnemyState.Chase);    
    }

    public override void Chase()
    {
        _animator.SetBool("Chase", true);
        _agent.isStopped = false;
        _agent.SetDestination(_target.position);
       

        float dist = Vector3.Distance(transform.position, _target.position);

        if (dist <= attackDistance && CanAttack())
        {
            _animator.SetBool("Chase", false);
            _agent.isStopped = true;
            SetState(EEnemyState.Attack);
        }
    }

    public override void Attack()
    {
        _animator.SetTrigger("Attack");
        cooldownTimer = attackCooldown;
        _agent.isStopped = true;
       SetState(EEnemyState.Idle);

    }
    public override void Arise()
    {
        SetState(EEnemyState.Chase);
    }
    private void DealDamage()
    {
        //напиши урон
    }
}