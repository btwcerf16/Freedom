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
        _hitEffect.Play();

        yield return new WaitUntil(() => !_hitEffect.isPlaying);

        _hitEffect.gameObject.SetActive(false);
        _spriteRenderer.enabled = true;
        ReturnIntoPool();
    }

    private IEnumerator ReturnAfterLaunch(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnIntoPool();
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
