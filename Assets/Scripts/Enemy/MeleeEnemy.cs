using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Combat")]
    [SerializeField] private float attackDistance = 1.4f;
    [SerializeField] private float attackCooldown = 1.8f;
    [SerializeField] private float orbitRadius = 2.5f;

    private float cooldownTimer;

    private void Update()
    {
        if (!_agent.isOnNavMesh) { Debug.Log("YTQWEQF"); return; } 
        if (!IsAgroed)
        {
            Idle();
            return;
        }

        switch (_state)
        {
            case EEnemyState.Idle:
                Idle();
                break;

            case EEnemyState.WaitingTurn:
                WaitingTurn();
                break;

            case EEnemyState.Chase:
                Chase();
                break;

            case EEnemyState.Attack:
                Attack();
                break;

            case EEnemyState.Cooldown:
                Cooldown();
                break;
        }
    }

    public override void StartAttackPermission()
    {
        base.StartAttackPermission();
        _state = EEnemyState.Chase;
    }

    public override bool CanAttack()
    {
        return IsAgroed && !IsCombatActive;
    }

    public override void Idle()
    {
        _agent.isStopped = true;
    }

    public override void WaitingTurn()
    {
        OrbitMovement(orbitRadius);

        float dist = Vector3.Distance(transform.position, _target.position);

       
        if (IsCombatActive)
            _state = EEnemyState.Chase;
    }

    public override void Chase()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_target.position);

        float dist = Vector3.Distance(transform.position, _target.position);

        if (dist <= attackDistance)
        {
            _agent.isStopped = true;  
            _state = EEnemyState.Attack;
        }
    }

    public override void Attack()
    {
        _agent.isStopped = true;

        // тут триггер анимации
        // Animator.SetTrigger("Attack");

        DealDamage();

        cooldownTimer = attackCooldown;
        _state = EEnemyState.Cooldown;
    }

    private void Cooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
            _state = EEnemyState.Chase;
    }

    private void DealDamage()
    {
        //напиши урон
    }
}