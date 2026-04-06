using UnityEngine;
[CreateAssetMenu(menuName = "Spells/ActiveSpells/BlowSpell", fileName = " BlowSpellConfig")]
public class BlowSpellConfig : SpellConfig
{
    public float BlowRadius;
    public float BlowDamage;
    public override Spell AddSpell(GameObject owner)
    {
        Spell spell = owner.AddComponent<BlowSpell>();
        spell.Initialize(this);
        return spell;
    }
}
