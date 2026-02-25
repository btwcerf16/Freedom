using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    private PlayerInventory _playerInventory;

    private Controls _controls;

    private PlayableActor _playerActor;

    private void Awake()
    {
        _playerActor = GetComponent<PlayableActor>();
        _controls = new Controls();
        _playerInventory = GetComponent<PlayerInventory>();
    }
    private void OnEnable()
    {
        _controls.Player.Enable();

        _controls.Player.PickUp.performed += OnPickup;
        _controls.Player.Throw.performed += OnThrow;
        _controls.Player.Slot1.performed += OnChooseFirstSlot;
        _controls.Player.Slot2.performed += OnChooseSecondSlot;
    }
    private void OnDisable()
    {
        _controls.Player.PickUp.performed -= OnPickup;
        _controls.Player.Throw.performed -= OnThrow;
        _controls.Player.Slot1.performed -= OnChooseFirstSlot;
        _controls.Player.Slot2.performed -= OnChooseSecondSlot;

        _controls.Player.Disable();
    }
    private void OnPickup(InputAction.CallbackContext context)
    {
        if(_playerActor == null) 
        {
            Debug.Log("Рука не найдена");
            return;
        }
        Weapon weapon = _playerActor.PlayerHand.GetLastWeaponInRange();
        if (weapon != null) 
        {
            _playerInventory.PickupWeapon(weapon);
        }
        
    }
    private void OnThrow(InputAction.CallbackContext context)
    {
        _playerInventory.ThrowWeapon();
    }
    private void OnChooseFirstSlot(InputAction.CallbackContext context)
    {
        _playerInventory.ChooseSlot(0);
    }
    private void OnChooseSecondSlot(InputAction.CallbackContext context)
    {
        _playerInventory.ChooseSlot(1);
    }
}

