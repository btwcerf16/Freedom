using System.Collections;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class RockOfIce : MonoBehaviour, IPoolable<RockOfIce>
{
    [SerializeField] private ObjectPool<RockOfIce> _pool;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private float _hitArea;
    [SerializeField] private Animator _animator;
    
    public float HitDamage;
    public GameObject Owner;
    public ActorStats OwnerStats;
    public EAttackType AttackType;
    public EDamageType EDamageType;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void StartFall(float damage, GameObject owner, ActorStats ownerStats) 
    {
        HitDamage = damage;
        Owner = owner;
        OwnerStats = ownerStats;
       

        _hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _hitEffect.Clear();
        _animator.SetTrigger("Fall");
    }
    private void Hit()
    {
        
        Collider2D[] targets = Physics2D.OverlapCircleAll(_hitPoint.position, _hitArea);
        foreach (Collider2D target in targets)
        {
            if (target.gameObject != Owner)
            {
                bool isCritical;
                float calculatedDamage =
                    DamageCalculator.CalculateDamage(HitDamage, AttackType,
                    EDamageType, OwnerStats, out isCritical);
                target.GetComponent<IDamageable>()?.GetDamage(calculatedDamage, isCritical);
                Debug.Log($"ÓÐÎÍ {calculatedDamage}" + target.name);
            }
        }

    }
    private IEnumerator ParticleCorutine()
    {
        _hitEffect.gameObject.SetActive(true);

        _hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _hitEffect.Clear();
        _hitEffect.Play();

        yield return new WaitUntil(() => !_hitEffect.IsAlive(true));
        ReturnIntoPool();
        _hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _hitEffect.Clear();
        
    }

    private void ReturnIntoPool()
    {
        if(_pool != null)
            _pool.ReturnObject(this);
    }

    public void SetPool(ObjectPool<RockOfIce> objectPool)
    {
        _pool = objectPool;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_hitPoint.position, _hitArea);
    }
}
