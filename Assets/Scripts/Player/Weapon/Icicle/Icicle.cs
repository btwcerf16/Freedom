using UnityEngine;

public class Icicle : Weapon
{
    
    public override bool CheckCondition()
    {
       return true;
    }

    public override void OnRelease()
    {
        Debug.Log("Конец");
        _isAttacking = false;
    }

    public void Hit()
    {
        _hand.Player.PlayerCinemachineCamera.GetComponent<CinemachineShake>().ShakeCamera(0.3f, 0.2f);
        _animator.SetTrigger("Hit");
    }

    public override void OnPress()
    {
        _isAttacking = true;
        Hit();
    }
}
