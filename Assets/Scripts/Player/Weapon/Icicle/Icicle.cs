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
    }

    public void Hit()
    {
        _animator.SetTrigger("Hit");
    }

    public override void OnPress()
    {
        Hit();
    }
}
