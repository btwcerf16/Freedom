using System.Collections;
using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private Animator _animator;


    public override void Launch(Vector2 direction, float speed, float damage, bool isCrit, float lifeTime)
    {
        base.Launch(direction, speed, damage, isCrit, lifeTime);
        StartCoroutine(destroyAfterLaunch(LifeTime));
    }
    public override void OnHit(Collider2D collider)
    {
        if (collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GetDamage(Damage, _isCrit);
        }

        StickIntoTarget(collider);
    }

    private void StickIntoTarget(Collider2D collider)
    {
        Debug.Log("Застрял" + collider.name);
        _animator.SetBool("IsSticked", true);
        RB2D.linearVelocity = Vector2.zero;
        RB2D.simulated = false;
        transform.SetParent(collider.transform);
    }
    IEnumerator destroyAfterLaunch(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        PoolsController.Instance.ArrowPool.ReturnObject(this);
    }
    
}
