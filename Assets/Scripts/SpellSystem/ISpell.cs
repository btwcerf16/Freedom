using UnityEngine;

public interface ISpell
{

    public void Cast(GameObject caster);
    public GameObject GetOwner();
    public void ChangeCooldown(float changedAmount);

}
