using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    public bool IsBusy;
    public Weapon SlotWeapon;
    public KeyCode Key;
    public TextMeshPro KeyText;



    public void SetWeapon(Weapon weapon)
    {
        Icon.enabled = true;
        Icon.sprite = weapon.GetSprite().sprite;
        SlotWeapon = weapon;
        IsBusy = true;
    }
    public void UnsetWeapon()
    {
        Icon.enabled = false;
        SlotWeapon = null;
        IsBusy = false;
    }


}
