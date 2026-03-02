using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    protected Transform _target;
    public ActorStats EnemyStats;
    public EffectHandler EnemyEffectHandler;
    [SerializeField]protected NavMeshAgent _agent;
    public bool IsAgroed;
    public bool IsCombatActive;
    public EEnemyType EnemyType;
    protected EnemyController _enemyController;
    protected EEnemyState _state;
    public EEnemyState CurrentState => _state;

    private void Start()
    {

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    public void Initialize(EnemyController enemyController, Transform target)
    {
        EnemyStats = GetComponent<ActorStats>();
        _agent = GetComponent<NavMeshAgent>();
        _target = target;
        _enemyController = enemyController;
        
    }
    public virtual void StartAttackPermission()
    {
        IsCombatActive = true;
        _state = EEnemyState.Chase;
    }
    public virtual void WaitingTurn() { }
    public virtual void Attack() { }
    public virtual void Chase() { }
    public virtual void Idle() { }
    public virtual bool CanAttack()
    {
        return false;
    }
    protected Vector3 GetOrbitPosition(float radius)
    {
        int index = _enemyController.GetEnemyIndex(this);
        int total = _enemyController.GetEnemiesCount();

        if (total == 0) return transform.position;

        float angle = (360f / total) * index;
        float rad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad) * radius,
            Mathf.Sin(rad) * radius,
            0f
        );

        return _target.position + offset;
    }
    protected void OrbitMovement(float radius)
    {
        Vector3 orbitPos = GetOrbitPosition(radius);
        _agent.isStopped = false;
        _agent.SetDestination(orbitPos);
    }
    protected virtual void OnDestroy() 
    {
        _enemyController.UnregisterEnemy(this);
    }
}