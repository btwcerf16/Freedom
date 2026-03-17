using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    
    public Animator EnemyAnimator;

    public EffectHandler EnemyEffectHandler;

    [SerializeField] protected GameObject _floatingDamage;
    
    [Header("States")]
    public bool IsAgroed;
    public bool IsCombatActive;
    public bool IsDead;
    public bool IsDisabled;

    [Header("Combat")]
    public EEnemyType EnemyType;
    public Transform EnemyTarget { get; private set; }
    [SerializeField] protected float _attackDistance = 1.4f;
    [SerializeField] protected Transform _attackPoint;
    public ActorStats EnemyStats;
    [SerializeField] protected bool _isBoss = false;
    [SerializeField] protected bool _isArised;
    [Header("Controller")]
    [SerializeField] protected NavMeshAgent _agent;
    protected EnemyController _enemyController;
    protected StateMachine EnemyStateMachine { get; set; }

    public event Action OnEnemyDeath;

    private Dictionary<Type, State> _states = new();
    public void Initialize(EnemyController enemyController, Transform target)
    {
        EnemyStats = GetComponent<ActorStats>();
        EnemyAnimator = GetComponent<Animator>();
        EnemyEffectHandler = GetComponent<EffectHandler>();
        

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        EnemyTarget = target;
        _enemyController = enemyController;

    }
    protected void RegisterState(State state)
    {
        _states[state.GetType()] = state;
    }
    protected T GetState<T>() where T: State
    {
        return (T)_states[typeof(T)];
    }
    public void ChangeState<T>() where T : State
    {
        EnemyStateMachine.ChangeState(GetState<T>());
    }

    public void Death()
    {
        OnEnemyDeath?.Invoke();
        Debug.Log("Умер");
        _enemyController.ReArise();
        if (_isBoss)
            SceneTransition.SwitchScene("MainMenu");

    }
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
    }

    private void EnableAgent()
    {
        if (_agent != null)
            _agent.enabled = true;
    }
    
    public virtual void Flip() { }
    public void SetTarget(Transform target)
    {
        EnemyTarget = target;
    }

}