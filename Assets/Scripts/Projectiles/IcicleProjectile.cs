using System.Collections;
using UnityEngine;

public class IcicleProjectile : Projectile, IPoolable<IcicleProjectile>
{
    private ObjectPool<IcicleProjectile> _pool;
    [SerializeField] private EffectData _effectData;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Launch(ProjectileData projectileData)
    {
        _spriteRenderer.enabled = true;
        RB2D.simulated = true;

        _hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _hitEffect.Clear();

        base.Launch(projectileData);

        StartCoroutine(ReturnAfterLaunch(LifeTime));
    }

    public override void OnHit(Collider2D collider2D)
    {
        base.OnHit(collider2D);

        if (collider2D.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            PlayHitAndReturn();
            return;
        }

        collider2D.GetComponent<EffectHandler>()?.AddEffect(_effectData);
        PlayHitAndReturn();
    }

    private void PlayHitAndReturn()
    {
        StopAllCoroutines(); 
        StartCoroutine(PlayEffectThenReturn());
    }


    private IEnumerator PlayEffectThenReturn()
    {
        _spriteRenderer.enabled = false;
        RB2D.simulated = false;

        _hitEffect.gameObject.SetActive(true);

        _hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _hitEffect.Clear();
        _hitEffect.Play();

        yield return new WaitUntil(() => !_hitEffect.IsAlive(true));

        _hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _hitEffect.Clear();

        _spriteRenderer.enabled = true;
        RB2D.simulated = true;

        ReturnIntoPool();
    }

    private IEnumerator ReturnAfterLaunch(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine(PlayEffectThenReturn());
        
    }

    private void ReturnIntoPool()
    {
        _pool.ReturnObject(this);
    }

    public void SetPool(ObjectPool<IcicleProjectile> objectPool)
    {
        _pool = objectPool;
    }
}
