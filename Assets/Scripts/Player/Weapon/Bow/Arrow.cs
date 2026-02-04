using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private Animator _animator;
    public override void OnHit(Collider2D collider)
    {
        if (collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GetDamage(Damage);
        }

        StickIntoTarget(collider);
    }

    private void StickIntoTarget(Collider2D collider)
    {
        _animator.SetBool("IsSticked", true);
        RB2D.linearVelocity = Vector2.zero;
        RB2D.simulated = false;

        transform.SetParent(collider.transform);
    }
}
