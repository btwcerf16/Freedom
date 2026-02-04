using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bow : Weapon
{
    
    private float _currentChargeTime;
    [SerializeField] private float _minChargeTime;
    [SerializeField] private float _maxChargeTime;
    [SerializeField] private float _chargeSpeed;
    [SerializeField] private Projectile _arrow;
    [SerializeField] protected Transform _shootPoint;
    [SerializeField] private float minArrowSpeed;
    [SerializeField] private float maxArrowSpeed;
    public override bool CanHold => true;

    public override bool CheckCondition()
    {
        if(_currentChargeTime >= _minChargeTime)
        {
            return true;
        }
        return false;
    }
    public override void OnPress()
    {
        _animator.SetFloat("ChargeSpeed", _chargeSpeed);
    }
    public override void OnHold()
    {
        
        _animator.SetBool("IsCharging", true);
        _currentChargeTime += Time.deltaTime * _chargeSpeed;
        if(_currentChargeTime >= _maxChargeTime)
        {
            _animator.SetBool("IsFullCharged", true);
        }
    }

    public override void OnRelease()
    {
        _animator.SetBool("IsCharging", false);
        _animator.SetBool("IsFullCharged", false);
        if (!CheckCondition())
        {
            _currentChargeTime = 0;
            return;
        }
        Shoot();
        _currentChargeTime = 0;
        Debug.Log("Выстрел на чарж" + _currentChargeTime);
        
    }
    private void Shoot()
    {
       
        Vector3 direction = _hand.transform.right;
        Debug.DrawRay(_shootPoint.position, direction * 2f, Color.red, 1f);
        Projectile projectile =
          Instantiate(_arrow, _shootPoint.position, Quaternion.identity);
        
        
        float chargePercent = Mathf.InverseLerp(_minChargeTime, _maxChargeTime, _currentChargeTime);
        float speed = Mathf.Lerp(minArrowSpeed, maxArrowSpeed, chargePercent);
        float damage = maxArrowSpeed * chargePercent / 8;
        projectile.Launch(direction, speed, damage);
    }

 
}