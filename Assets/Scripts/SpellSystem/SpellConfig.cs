using UnityEngine;

public abstract class SpellConfig : ScriptableObject
{
    public string SpellName;
    public string SpellDescription;
    public bool IsPassive = false;
    public float CooldownTime;
    public Sprite SpriteIcon;
    public abstract Spell AddSpell(GameObject owner);

}
