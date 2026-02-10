using UnityEngine;

public class RustyScrap : Weapon
{
    public override bool CheckCondition()
    {
        return true;
    }

    public override void OnPress()
    {
        _animator.SetTrigger("Attack");
    }
}
