using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject IconGameObject;
    public Image Icon;
    public bool IsBusy;
    public Weapon SlotWeapon;
    public KeyCode Key;
    public TextMeshPro KeyText;


    private void Awake()
    {
        Icon = IconGameObject.GetComponent<Image>();
        
    }
    public void SetWeapon(Weapon weapon)
    {
        Icon.enabled = true;
        Icon.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
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
