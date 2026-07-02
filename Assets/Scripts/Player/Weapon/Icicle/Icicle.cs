
using UnityEngine;

public class Icicle : Weapon
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 1.0f;
    [SerializeField] private EffectData _effectData;

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
            Debug.Log($"{target.name} {target.GetType()}");
            if (target.CompareTag("Enemy"))
            {
                
                SpellCastData spellCastData = new()
                {
                    Direction = transform.right,
                    Caster = Owner,
                    Target = target.gameObject,
                    Position = target.transform.position

                };
                _spell.Cast(spellCastData);
                bool isCritical;
                float calculatedDamage = 
                    DamageCalculator.CalculateDamage(_hand.Player.PlayerActorStats.AttackDamage.CurrentValue, AttackType,
                    DamageType, _hand.Player.PlayerActorStats, out isCritical);

                target.GetComponent<IDamageable>()?.GetDamage(calculatedDamage, isCritical);
                target.GetComponent<EffectHandler>()?.AddEffect(_effectData);
            }
        }

        
    }

    public override void OnPress()
    {
        _isAttacking = true;
        _animator.SetTrigger("Hit");

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }
}
