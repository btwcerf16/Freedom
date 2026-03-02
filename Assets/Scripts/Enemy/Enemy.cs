using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Common Settings")]
    public float detectionRadius = 5f;
    public float attackCooldown = 1.5f;
    public Transform target;

    protected NavMeshAgent agent;
    protected bool canAttack = true;
    [SerializeField]protected bool isAggro = false;
    protected EnemyController _enemyController;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void SetManager(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }

    public void TriggerAggro()
    {
        isAggro = true;
        Aggro();
    }

    protected virtual void Update()
    {
        if (!isAggro || target == null)
        {
            Idle();
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        Act(distance);
    }

    protected IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    protected abstract void Act(float distance);

    protected virtual void Idle()
    {
        if (agent.isOnNavMesh)
            agent.ResetPath();
        // TODO: добавить анимацию idle
    }

    protected virtual void Aggro()
    {

        Debug.Log($"{name} агрится на игрока!");
    }

    protected virtual void Attack()
    {
        Debug.Log($"{name} атакует!");
    }
}