using UnityEngine;

public class Bow : Weapon
{
    
    private float _currentChargeTime;
    [SerializeField] private float _minChargeTime;
    [SerializeField] private float _maxChargeTime;
    [SerializeField] private float _chargeSpeed;
    
    public override bool CanHold => true;

    public override bool CheckCondition()
    {
        if(_currentChargeTime >= _minChargeTime)
        {
            return true;
        }
        return false;
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
        if (CheckCondition()) 
        {
            Debug.Log("Выстрел на чарж" + _currentChargeTime);
            _currentChargeTime = 0;
        }
        
    }
}
