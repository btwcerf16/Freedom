using DG.Tweening;
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

    private Sequence _animation;

    public void SetWeapon(Weapon weapon)
    {
        Icon.enabled = true;
        Icon.sprite = weapon.GetSprite().sprite;
        SlotWeapon = weapon;
        IsBusy = true;

        _animation?.Kill();

        transform.localScale = Vector3.one;
        Icon.transform.localScale = Vector3.one;

        _animation = DOTween.Sequence();

        _animation.Append(transform.DOScale(1.15f, 0.12f).SetEase(Ease.OutQuad));
        _animation.Join(Icon.transform.DOScale(1.3f, 0.12f).SetEase(Ease.OutQuad));

        _animation.Append(transform.DOScale(0.97f, 0.08f));
        _animation.Join(Icon.transform.DOScale(0.9f, 0.08f));

        _animation.Append(transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack));
        _animation.Join(Icon.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack));

    }
    public void UnsetWeapon()
    {
        _animation?.Kill();

        _animation = DOTween.Sequence();

        _animation.Append(transform.DOScale(1.05f, 0.08f));
        _animation.Join(Icon.transform.DOScale(1.1f, 0.08f));

        _animation.Append(transform.DOScale(0f, 0.18f).SetEase(Ease.InBack));
        _animation.Join(Icon.transform.DOScale(0f, 0.18f).SetEase(Ease.InBack));

        _animation.OnComplete(() =>
        {
            Icon.enabled = false;
            SlotWeapon = null;
            IsBusy = false;

            transform.localScale = Vector3.one;
            Icon.transform.localScale = Vector3.one;
        });

    }
    private void OnDestroy()
    {
        _animation.Kill();
    }

}
