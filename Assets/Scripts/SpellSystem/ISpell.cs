using UnityEngine;

public interface ISpell
{
    public GameObject SpellOwner {  get; set; }

    public void Cast(GameObject caster);
    public void UpdateEffect(GameObject target);
    public void SetOwner(GameObject owner);

}
