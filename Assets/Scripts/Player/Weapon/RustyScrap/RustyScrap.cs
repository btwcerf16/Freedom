using UnityEngine;

public class RustyScrap : Weapon
{
    public override bool CheckCondition()
    {
        return true;
    }

    public override void OnPress()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
    }
    public override void OnRelease()
    {
        _isAttacking = false;
    }
}
