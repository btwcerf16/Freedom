using System;
using System.Collections;
using UnityEngine;

public class RustyScrap : Weapon
{
    [SerializeField] private float _attackCooldown = .3f;
    [SerializeField] private bool _isCooldown;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 1.0f;

    [SerializeField] private float _knockbackSrength = 10.0f;
    [SerializeField] private float _knockbackTime = 0.3f;
    public override bool CheckCondition()
    {
        return _isCooldown;
    }

    public override void OnPress()
    {
        if (!CheckCondition())
        {
            _hand.Player.PlayerCinemachineCamera.GetComponent<CinemachineShake>().ShakeCamera(0.8f, 0.2f);
            _isAttacking = true;
            _animator.SetTrigger("Attack");
            _isCooldown = transform;
            StartCoroutine(cooldownReset());
        }

    }
    public override void OnRelease()
    {
        _isAttacking = false;
    }
    private void Hit()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius);
        foreach (Collider2D target in targets)
        {
            if (target.CompareTag("Enemy"))
            {
                Debug.Log("Попал");
                target.GetComponent<IDamageable>().GetDamage(_hand.Player.PlayerActorStats.CurrentDamageAttack, true);
                Knockback(target);
            }

        }
    }
    private void Knockback(Collider2D target)
    {
        IForceReceiver forceReceiver = target.GetComponent<IForceReceiver>();

        if (forceReceiver != null)
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            forceReceiver.ApplyForce(dir, _knockbackSrength, _knockbackTime);
        }
    }
    IEnumerator cooldownReset()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _isCooldown = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }


}
