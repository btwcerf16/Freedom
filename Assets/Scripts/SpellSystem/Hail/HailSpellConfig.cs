using UnityEngine;
[CreateAssetMenu(menuName = "Spells/LastSpells/HailSpell", fileName = "HailSpellConfig")]
public class HailSpellConfig : SpellConfig
{
    public int PoolSize = 6;
    public float Radius = 20f;
    public RockOfIce RockOfIcePrefab;
    public float Damage = 10f;
    public float TimeBeforeFall = 0.5f;
    public float DamageMultiplyer; //each 3 time 

    public override Spell AddSpell(GameObject owner)
    {
        Spell spell = owner.AddComponent<HailSpell>();
        spell.Initialize(this);
        return spell;
    }
}
