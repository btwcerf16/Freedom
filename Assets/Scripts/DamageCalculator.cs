using UnityEngine;
public static class DamageCalculator
{
    public static float CalculateDamage(
        float baseDamage,
        EAttackType attackType,
        EDamageType damageType,
        ActorStats actorStats,
        out bool isCrit)
    {
        float damage = Mathf.Max(0, baseDamage);

        damage *= GetAttackModifier(attackType, actorStats);
        damage *= GetDamageModifier(damageType, actorStats);

        isCrit = Random.value * 100 <= actorStats.CritChance.CurrentValue;

        if (isCrit)
            damage *= Mathf.Max(1f, actorStats.CritDamageMultiplier.CurrentValue);

        return damage;
    }

    private static float GetAttackModifier(EAttackType type, ActorStats actorStats)
        {
            float modifier = 1f;

            if (type.HasFlag(EAttackType.Magic))
                modifier *= actorStats.MagicDamageMultiplier.CurrentValue;

            if (type.HasFlag(EAttackType.Range))
                modifier *= actorStats.RangeDamageMultiplier.CurrentValue;

            return modifier;
        }

        private static float GetDamageModifier(EDamageType type, ActorStats actorStats)
        {
            float modifier = 1f;

            if (type.HasFlag(EDamageType.Piercing))
                modifier *= actorStats.PiercingDamageMultiplier.CurrentValue;

            if (type.HasFlag(EDamageType.Bludgeoning))
                modifier *= actorStats.BludgeoningDamageMultiplier.CurrentValue;
            if (type.HasFlag(EDamageType.Slashing))
                modifier *= actorStats.SlashingDamageMultiplier.CurrentValue;

            return modifier;
        }
}
