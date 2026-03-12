using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask _weaponLayer;
    [SerializeField] private bool _isActive;
    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, _weaponLayer);

        if (hit.collider != null)
        {
            Weapon weapon = hit.collider.GetComponent<Weapon>();
            if (weapon != null && !weapon.IsRaised)
            {
                ControlTooltip.Instance.Show(weapon.Description, weapon.transform.position, weapon);
                _isActive = true;
            }
        }
        else
        {
            if (_isActive)
            {
                ControlTooltip.Instance.HideAll();
                _isActive = false;
            }

        }

    }
}