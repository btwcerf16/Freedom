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
    public bool IsAgroed;
    public bool IsCombatActive;
    public EEnemyType EnemyType;
    protected EnemyController _enemyController;
    [SerializeField] protected EEnemyState _state;
    public bool IsDead;
 
    public EEnemyState CurrentState => _state;

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
        Debug.Log("Coroutine started");
        StartCoroutine(SetStateCoroutine(state, delay));
    }
    IEnumerator SetStateCoroutine(EEnemyState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Coroutine ended> " + state.ToString() );
        SetState(state);
    }
}