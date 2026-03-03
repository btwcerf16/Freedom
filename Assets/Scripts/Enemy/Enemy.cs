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
    [SerializeField] protected EEnemyState _state;
    [SerializeField] protected float orbitRadius = 2.5f;
    [SerializeField] protected float orbitSpeed = 20f;
    [SerializeField] protected float angleRandomOffset = 25f;
    private float _currentAngle;

    private float _baseAngle;
    private float _randomOffset;
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
        _baseAngle = (360f / _enemyController.GetEnemiesCount())
                     * _enemyController.GetEnemyIndex(this);

        _randomOffset = Random.Range(-angleRandomOffset, angleRandomOffset);

        _currentAngle = _baseAngle + _randomOffset;

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
    public virtual void EndAttackPermission() { }
    public virtual bool CanAttack()
    {
        return true;
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
    protected void OrbitMovement()
    {
        if (_target == null) return;
        if (!_agent.isOnNavMesh) return;

        _currentAngle += orbitSpeed * Time.deltaTime;

   
        if (_currentAngle > 360f)
            _currentAngle -= 360f;

        float rad = _currentAngle * Mathf.Deg2Rad;

        float radiusOffset = Mathf.Sin(Time.time * 2f) * 0.3f;
        float currentRadius = orbitRadius + radiusOffset;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad) * currentRadius,
            Mathf.Sin(rad) * currentRadius,
            0f
        );

        Vector3 orbitPos = _target.position + offset;

        if (!float.IsNaN(orbitPos.x) && !float.IsNaN(orbitPos.y))
        {
            _agent.isStopped = false;
            _agent.SetDestination(orbitPos);
        }
    }
    public void SetState(EEnemyState state)
    {
        _state = state;
    }
    protected virtual void OnDestroy() 
    {
        _enemyController.UnregisterEnemy(this);
    }
}