using UnityEngine;

public class IcicleExplosionSpellConfig : SpellConfig
{
    public GameObject IciclePrefab;
    public float IcicleDamage;
    public override Spell AddSpell(GameObject owner)
    {
        Spell spell = owner.AddComponent<IcicleExplosionSpell>();
        spell.Initialize(this);
        return spell;
    }
}
