using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(CircleCollider2D))]
public class Hand : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxRadius = 1.5f;
    [SerializeField] private List<Weapon> _weaponsInRange = new();
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Sprite _baseSpriteImage;
    [SerializeField] private float maxAngle = 70f;
    [SerializeField] private bool _isFacingRight = true;

    private Camera _camera;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _camera = Camera.main;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _sprite.sprite = _baseSpriteImage;
    }

    private void Update()
    {
        Vector3 mouseWorld =
            _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0;

        Vector3 dir = mouseWorld - player.position;

        if (dir.magnitude > maxRadius)
            dir = dir.normalized * maxRadius;



        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float baseAngle = _isFacingRight ? 0f : 180f;
        float delta = Mathf.DeltaAngle(baseAngle, angle);
        delta = Mathf.Clamp(delta, -maxAngle, maxAngle);
        float finalAngle = baseAngle + delta;
        Vector3 clampedDir = new Vector3(
    Mathf.Cos(finalAngle * Mathf.Deg2Rad),
    Mathf.Sin(finalAngle * Mathf.Deg2Rad)
) * Mathf.Min(dir.magnitude, maxRadius);

        transform.position = player.position + clampedDir;
        transform.rotation = Quaternion.Euler(0, 0, finalAngle);
        Flip(clampedDir);
    }
    private void Flip(Vector3 dir)
    {
        if (dir.x < 0) 
        {
            _sprite.transform.localScale = new Vector3(-1, 1, 1);
        }

        else
        {
            _sprite.transform.localScale = Vector3.one;
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
            return;
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
        if (player == null) return;

        // радиус
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.position, maxRadius);

        // направление вперёд (ось X)
        Vector3 forward = _isFacingRight ? Vector3.right : Vector3.left;

        // границы углов

        Vector3 leftBorder = Quaternion.Euler(0, 0, -maxAngle) * forward;
        Vector3 rightBorder = Quaternion.Euler(0, 0, maxAngle) * forward;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(player.position, player.position + leftBorder * maxRadius);
        Gizmos.DrawLine(player.position, player.position + rightBorder * maxRadius);
    }

}

