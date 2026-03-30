using UnityEngine;

public abstract class SpellConfig : ScriptableObject
{
    public string SpellName;
    public string SpellDescription;
    public bool IsPassive = false;
    public string CooldownTimer;

    public abstract ISpell AddSpell();

}
