using UnityEngine;
[CreateAssetMenu(menuName = "Spells/ActiveSpells/IcicleExplosionSpell", fileName = " IcicleExplosionSpellConfig")]
public class IcicleExplosionSpellConfig : SpellConfig
{
    public IcicleProjectile IciclePrefab;
  
    public int PoolSize = 30;
    public int IcicleCount = 8;
    public float Radius = 2f;
    public float ProjectileSpeed = 5f;
    public float Damage = 10f;
    public EAttackType AttackType;
    public EDamageType DamageType;
    public override Spell AddSpell(GameObject owner)
    {
        Spell spell = owner.AddComponent<IcicleExplosionSpell>();
        spell.Initialize(this);
        return spell;
    }
}
