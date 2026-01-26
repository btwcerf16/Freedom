using UnityEngine;

public class Icicle : Weapon
{
    
    public override bool CheckCondition()
    {
       return true;
    }

    public override void Use()
    {
        Debug.Log("Удар сосулькой");
    }
}
