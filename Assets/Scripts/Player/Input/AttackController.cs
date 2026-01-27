using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    private Controls _controls;
    private PlayerInventory _playerInventory;
    private bool _isHolding;
    private void Awake()
    {
        _controls = new Controls();
        _playerInventory = GetComponent<PlayerInventory>();

    }
    private void OnEnable()
    {
        _controls?.Player.Enable();
        _controls.Player.Attack.started += OnAttackStarted;
        _controls.Player.Attack.canceled += OnAttackCanceled;
        

    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (_playerInventory.ActiveWeapon == null)
            return;
        _isHolding = true;
        _playerInventory.ActiveWeapon.OnPress();
        
    }
    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        if (_playerInventory.ActiveWeapon == null)
            return;
        _isHolding =false;
        _playerInventory.ActiveWeapon.OnRelease();
    }
    private void Update()
    {
        if (_playerInventory.ActiveWeapon == null)
            return;
        if (_isHolding && _playerInventory.ActiveWeapon.CanHold) {
            _playerInventory.ActiveWeapon.OnHold();
        }
    }

    private void OnDisable()
    {
        _controls.Player.Attack.started -= OnAttackStarted;
        _controls.Player.Attack.canceled -= OnAttackCanceled;
        _controls?.Player.Disable();
    }
    
}
