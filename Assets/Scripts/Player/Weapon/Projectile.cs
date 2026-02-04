using UnityEngine;
using UnityEngine.Rendering;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float LifeTime;
    [SerializeField] protected float ProjectileSpeed;
    [SerializeField] protected float Damage;
    [SerializeField] protected LayerMask layerMask;
    protected Rigidbody2D RB2D;

    private void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>();
        RB2D.gravityScale = 0;
        RB2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Start()
    {
        Destroy(gameObject, LifeTime);
    }
    public virtual void Launch(Vector2 direction, float speed, float damage)    {
        RB2D.linearVelocity = direction.normalized * speed;
        RotateToVelocity();
        Damage = damage;
    }
    public virtual void OnHit(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent<IDamageable>(out IDamageable enemy) )
        {
            enemy.GetDamage(Damage);
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInLayerMask(collision.gameObject.layer, layerMask))
            return;

        OnHit(collision);
    }
    protected bool IsInLayerMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
    protected void RotateToVelocity()
    {
        if (RB2D.linearVelocity.sqrMagnitude < 0.001f)
            return;

        float angle = Mathf.Atan2(RB2D.linearVelocity.y, RB2D.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log(RB2D.linearVelocity);
    }
}
