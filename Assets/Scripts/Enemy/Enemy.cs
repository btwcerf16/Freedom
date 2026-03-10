using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    protected Transform _target;
    protected Animator _animator;
    public ActorStats EnemyStats;
    public EffectHandler EnemyEffectHandler;
    [SerializeField]protected NavMeshAgent _agent;
    public EEnemyType EnemyType;
    protected EnemyController _enemyController;
    [SerializeField] protected Transform _attackPoint;

    [SerializeField] protected GameObject _floatingDamage;
    
    [Header("States")]
    [SerializeField] protected EEnemyState _state;
    public bool IsAgroed;
    public bool IsCombatActive;
    public bool IsDead;
    //[Header ("Separation Settings")]
    //[SerializeField] private float _separationRadius = 0.7f;
    //[SerializeField] private float _separationForce = 2f;
    //[SerializeField] private LayerMask _enemyLayer;
    public EEnemyState CurrentState => _state;
    [Header("Combat")]
    [SerializeField] protected float _attackDistance = 1.4f;

    [SerializeField] protected bool _isBoss = false;
    private void Start()
    {
  
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
    }
    public void Initialize(EnemyController enemyController, Transform target)
    {
        
        EnemyStats = GetComponent<ActorStats>();
        _target = target;
        _enemyController = enemyController;
        _animator = GetComponent<Animator>();


    }
    public virtual void Death()
    {
        _enemyController.ReArise();
        
    }
    public virtual void WaitingTurn() { }
    public virtual void Attack() { }
    public virtual void Chase() { }
    public virtual void Idle() { }
    public virtual void Arise() {}
    public virtual bool CanAttack()
    {
        return true;
    }
    public virtual void EnableAfterSpawn()
    {
        if (_agent == null)
            _agent = GetComponent<NavMeshAgent>();
        
        _agent.enabled = false;
        Invoke(nameof(EnableAgent), 0.2f);
        SetState(EEnemyState.Chase);
    }

    private void EnableAgent()
    {
        if (_agent != null)
            _agent.enabled = true;
    }
    public void SetState(EEnemyState state)
    {
        _state = state;
    }
    public void SetStateDelayed(EEnemyState state, float delay)
    {
        
        StartCoroutine(SetStateCoroutine(state, delay));
    }
    IEnumerator SetStateCoroutine(EEnemyState state, float delay)
    {
        yield return new WaitForSeconds(delay);

        SetState(state);
    }
    public virtual void Flip() { }


}