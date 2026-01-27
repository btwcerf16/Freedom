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
        Debug.Log("Удар сосулькой");
    }

    public override void OnPress()
    {
        Hit();
    }
}
