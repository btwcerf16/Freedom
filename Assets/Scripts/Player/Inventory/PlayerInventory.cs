using NUnit.Framework;
using System.Net;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlots = new InventorySlot[2];
    private Weapon _activeWeapon;
    private int _activeIndexSlot;
    public Weapon ActiveWeapon { get { return _activeWeapon; }}
    [SerializeField] private Hand _hand;

    public void PickupWeapon(Weapon weapon)
    {
        if (!_inventorySlots[_activeIndexSlot].IsBusy)
        {
            
            _inventorySlots[_activeIndexSlot].SetWeapon(weapon);
            ChooseSlot(_activeIndexSlot);
            return;
        }
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (!_inventorySlots[i].IsBusy)
            {
                _inventorySlots[i].SetWeapon(weapon);
                ChooseSlot(i);
                return;
            }
        }
            Debug.Log("Инвентарь полон");
        
    }
    public void ThrowWeapon()
    {
        _hand.UnequipWeapon();
        _inventorySlots[_activeIndexSlot].UnsetWeapon();
    }
    public void ChooseSlot(int indexSlot)
    {
        _activeIndexSlot = indexSlot;
        Weapon weapon = _inventorySlots[_activeIndexSlot].SlotWeapon;
        if (weapon != null)
        {
            _activeWeapon = weapon;
            _hand.EquipWeapon(weapon);
        }
        else
        {
            _activeWeapon = null;
            _hand.HideWeapon();
        }

        Debug.Log("Выбрал " + _activeIndexSlot);
    }

}
