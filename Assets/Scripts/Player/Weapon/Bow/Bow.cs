
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
    [SerializeField] private float _baseOrthographicSize;
    [SerializeField] private float _maxOrthographicSize = 10;
    [SerializeField] private bool _isSlowedDown;
    [SerializeField,Range(-10, 0)] private float _moveSpeedSlow = -2.5f; //насколько замедляет владельца пока он натягивает тетиву
    private PlayableActor _playableActor;
    private StatModifier _statModifier;
    public override bool CanHold => true;
    private void Start()
    {
        _statModifier = new(ModifierType.Flat, _moveSpeedSlow, this);
    }
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
        if (_playableActor == null)
            return;
        _animator.SetFloat("ChargeSpeed", _chargeSpeed);
    }
    
    public override void AttachToHand(Transform hand)
    {
        base.AttachToHand(hand);
        _playableActor = Owner.GetComponent<PlayableActor>();
        _baseOrthographicSize = _playableActor.PlayerCinemachineCamera.Lens.OrthographicSize;
    }
    public override void DetachFromHand()
    {
        base.DetachFromHand();
        _playableActor.PlayerCinemachineCamera.Lens.OrthographicSize = _baseOrthographicSize;
        _playableActor = null;
    }
    public override void HideFromHand()
    {
        base.HideFromHand();
        OnRelease();
    }
    public override void OnHold()
    {

        if( !_isSlowedDown )
        {
            _playableActor.PlayerActorStats.MoveSpeed.AddModifier(_statModifier);
        }
         
        _isSlowedDown = true;
        _isAttacking = true;
        _animator.SetBool("IsCharging", true);
        _currentChargeTime += Time.deltaTime * _chargeSpeed;
        if(_currentChargeTime >= _maxChargeTime)
        {
            _animator.SetBool("IsFullCharged", true);
        }
        
        float orthographicSize = Mathf.InverseLerp(_baseOrthographicSize, _maxOrthographicSize, _currentChargeTime);
        _playableActor.PlayerCinemachineCamera.Lens.OrthographicSize = orthographicSize;
        
        
    }

    public override void OnRelease()
    {
        _isSlowedDown = false;
        _playableActor.PlayerActorStats.MoveSpeed.RemoveModifier(_statModifier);
        _playableActor.PlayerCinemachineCamera.Lens.OrthographicSize = _baseOrthographicSize;
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
        _isAttacking = false;

    }
    private void Shoot()
    {
       
        Vector3 direction = _hand.transform.right;
        Debug.DrawRay(_shootPoint.position, direction * 2f, Color.red, 1f);
        //Projectile projectile =
        //  Instantiate(_arrow, _shootPoint.position, Quaternion.identity);
        Arrow arrow = PoolsController.Instance.ArrowPool.GetObject();
        arrow.transform.position = _shootPoint.position;
        arrow.transform.rotation = Quaternion.identity;
        float chargePercent = Mathf.InverseLerp(_minChargeTime, _maxChargeTime, _currentChargeTime);
       
        float speed = Mathf.Lerp(minArrowSpeed, maxArrowSpeed, chargePercent);
        float damage = AttackDamage * chargePercent;
        bool isCritical;
        float calculatedDamage = DamageCalculator.CalculateDamage(damage, AttackType, DamageType, _hand.Player.PlayerActorStats, out isCritical);
        Debug.Log(isCritical);

        arrow.Launch(direction, speed, calculatedDamage, isCritical, 10.0f);
    }


}