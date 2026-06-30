using System.Collections;
using UnityEngine;

public class Arrow : Projectile, IPoolable<Arrow>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Enemy target;
    [SerializeField] private EffectData _effectData;
    private ObjectPool<Arrow> _pool;
    public override void Launch(ProjectileData projectileData)
    {
        base.Launch(projectileData);
        StartCoroutine(returnAfterLaunch(LifeTime));
    }
    public override void OnHit(Collider2D collider)
    {
        if (collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GetDamage(Damage, _isCrit);
            if (collider.TryGetComponent<Enemy>(out var enemy))
            {

                target = enemy;
                if (_effectData != null)
                    enemy.EnemyEffectHandler.AddEffect(_effectData);

                target.OnEnemyDeath += ReturnIntoPool;
            }
            Debug.Log("Вышел");
        }
        Debug.Log("Стик");
        StickIntoTarget(collider);
    }

    private void StickIntoTarget(Collider2D collider)
    {

        Debug.Log("Застрял " + collider.name);
        _animator.SetBool("IsSticked", true);
        RB2D.linearVelocity = Vector2.zero;
        RB2D.simulated = false;
        transform.SetParent(collider.transform);
    }
    IEnumerator returnAfterLaunch(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        ReturnIntoPool();
        Debug.Log("Вернулся в пул");
    }
    private void ReturnIntoPool()
    {
        Debug.Log("Проверка");
        if (target != null)
        {
            Debug.Log("Прошел");
            target.OnEnemyDeath -= ReturnIntoPool;
            target = null;
            PoolsController.Instance.ArrowPool.ReturnObject(this);
            return;
        }
        PoolsController.Instance.ArrowPool.ReturnObject(this);

    }

    public void SetPool(ObjectPool<Arrow> objectPool)
    {
        _pool = objectPool;
    }
}
