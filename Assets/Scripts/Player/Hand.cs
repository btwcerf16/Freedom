using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(CircleCollider2D))]
public class Hand : MonoBehaviour
{
    [SerializeField] private PlayableActor _player;
    [SerializeField] private float maxRadius = 1.5f;
    [SerializeField] private List<Weapon> _weaponsInRange = new();
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Sprite _baseSpriteImage;
    [SerializeField] private bool _isFacingRight = true;
    public bool IsRightSide => _isFacingRight;
    public PlayableActor Player => _player;
    private Camera _camera;
    private SpriteRenderer _sprite;

    public void Initialize(PlayableActor playableActor)
    {
        _player = playableActor;
    }
    private void Awake()
    {
        
        _camera = Camera.main;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _sprite.sprite = _baseSpriteImage;
    }
    

    private void Update()
    {
        if (_player == null)
            return;
        Vector3 mouseWorld =
            _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0;

        Vector3 dir = mouseWorld - _player.transform.position;

        if (dir.magnitude > maxRadius)
            dir = dir.normalized * maxRadius;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.position = _player.transform.position + dir;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _isFacingRight = dir.x >= 0;

        Flip(dir);
    }
    private void Flip(Vector3 dir)
    {
        if (dir.x < 0) 
        {
            _sprite.transform.localScale = new Vector3(-1, 1, 1);
            if(_currentWeapon != null)
                _currentWeapon.transform.localScale = new Vector3(1, -1, 1);
        }

        else
        {
            _sprite.transform.localScale = Vector3.one;
            if (_currentWeapon != null)
                _currentWeapon.transform.localScale = Vector3.one;
        }
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Weapon>(out Weapon weapon))
        {
            if (!_weaponsInRange.Contains(weapon))
                _weaponsInRange.Add(weapon);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Weapon>(out Weapon weapon))
        {
            _weaponsInRange.Remove(weapon);
        }
    }
    public void EquipWeapon(Weapon weapon)
    {
        if(_currentWeapon != null)
        {
            _currentWeapon.HideFromHand();
        }
        _currentWeapon = weapon;
        _currentWeapon.AttachToHand(transform);
        _sprite.enabled = false;
    }
    public void UnequipWeapon()
    {
        if(_currentWeapon  == null)
        {
            return;
        }
        _currentWeapon.DetachFromHand();
        _currentWeapon = null;
        _sprite.enabled = true;
    }
    public void HideWeapon()
    {
        if (_currentWeapon == null)
        {
            return;
        }
        _currentWeapon.HideFromHand();
        
        _sprite.enabled = true;

    }
    public Weapon GetLastWeaponInRange()
    {
        if (_weaponsInRange.Count == 0)
        {
            return null;
        }

        return _weaponsInRange[_weaponsInRange.Count - 1];
    }
    private void OnDrawGizmos()
    {
        if (_player == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_player.transform.position, maxRadius);
    }

}

