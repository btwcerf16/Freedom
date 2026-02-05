using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Collider2D _collider;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;
    [SerializeField] protected float zRotationOffset;
    protected Hand _hand;
    public virtual bool CanHold => false;
    protected virtual void Awake()
    {

        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// Проверка: можно ли выполнить атаку ПРЯМО СЕЙЧАС
    /// </summary>
    public abstract bool CheckCondition();

    /// <summary>
    /// Выполнить атаку единажды при нажатии ЛКМ
    /// </summary>
    public virtual void OnPress() { }
    /// <summary>
    /// Выполнять атаку пока ЛКМ зажата
    /// </summary>
    public virtual void OnHold() { }
    public virtual void OnRelease() { }
    public virtual void Throw(Vector2 direction, float force)
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void AttachToHand(Transform hand)
    {
        _hand = hand.GetComponent<Hand>();
        _rb.simulated = false;
        _collider.enabled = false;
        _spriteRenderer.enabled = true;
        transform.SetParent(hand, false);
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, hand.rotation.z);
    }

    public void DetachFromHand()
    {
        transform.SetParent(null);
        _spriteRenderer.enabled = true;
        _rb.simulated = true;
        _collider.enabled = true;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public void HideFromHand()
    {
        _spriteRenderer.enabled = false;
        _rb.simulated = false;
        _collider.enabled = false;
    }
    public SpriteRenderer GetSprite()
    {
        return _spriteRenderer;
    }

}
