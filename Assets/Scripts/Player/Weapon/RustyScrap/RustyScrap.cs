using UnityEngine;

public class RustyScrap : Weapon
{
    public override bool CheckCondition()
    {
        return true;
    }

    public override void OnPress()
    {
        _hand.Player.PlayerCinemachineCamera.GetComponent<CinemachineShake>().ShakeCamera(0.8f, 0.2f);
        _isAttacking = true;
        _animator.SetTrigger("Attack");
    }
    public override void OnRelease()
    {
        _isAttacking = false;
    }
}
