using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Collider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Проверка: можно ли выполнить атаку ПРЯМО СЕЙЧАС
    /// </summary>
    public abstract bool CheckCondition();

    /// <summary>
    /// Выполнить атаку (Use вызывается только если CheckCondition == true)
    /// </summary>
    public abstract void Use();

    public virtual void Throw(Vector2 direction, float force)
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
    public void AttachToHand(Transform hand)
    {
        _rb.simulated = false;
        _collider.enabled = false;
        _spriteRenderer.enabled = true;
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void DetachFromHand()
    {
        transform.SetParent(null);
        _spriteRenderer.enabled = true;
        _rb.simulated = true;
        _collider.enabled = true;
    }
    public void HideFromHand()
    {
        _spriteRenderer.enabled = false;
        _rb.simulated = false;
        _collider.enabled = false;
    }

}
