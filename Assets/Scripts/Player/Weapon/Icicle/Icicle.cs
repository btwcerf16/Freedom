
using UnityEngine;

public class Icicle : Weapon
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 1.0f;
    [SerializeField] private EffectData _effectData;
    [SerializeField] private SpellConfig _spellConfig;
    [SerializeField] private Spell _spell;
    public override bool CheckCondition()
    {
       return true;
    }

    public override void OnRelease()
    {
        Debug.Log("Конец");
        _isAttacking = false;
    }
    private void Start()
    {

        _spell = _spellConfig?.AddSpell(gameObject);

    }
    public void Hit()
    {
        _hand.Player.PlayerCinemachineCamera.GetComponent<CinemachineShake>().ShakeCamera(0.1f, 0.2f);
        Collider2D[] targets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Enemy"))
            {
                Debug.Log("Попал");
                target.GetComponent<IDamageable>()?.GetDamage(_hand.Player.PlayerActorStats.CurrentDamageAttack, false);
                target.GetComponent<EffectHandler>().AddEffect(_effectData);
            }
        }
    }

    public override void OnPress()
    {
        _isAttacking = true;
        _animator.SetTrigger("Hit");
        _spell.Cast();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }
}
