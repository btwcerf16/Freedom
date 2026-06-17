using UnityEngine;

public class IcicleExplosionSpell : Spell
{
    public override void Cast(SpellCastData spellCastData)
    {
        base.Cast(spellCastData);

    }

    public override void OnEndCast()
    {
        base.OnEndCast();
    }

    public override void SetCooldownTime(float oldCooldownTime, float cooldownTime)
    {
        base.SetCooldownTime(oldCooldownTime, cooldownTime);
    }
}
