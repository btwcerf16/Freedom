using UnityEngine;
using static UnityEngine.Rendering.STP;

public class IcicleExplosionSpell : Spell
{
    private ObjectPool<IcicleProjectile> _pool;

    private IcicleExplosionSpellConfig _config => (IcicleExplosionSpellConfig)SpellData;

    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);
        if (_pool == null)
        {
            _pool = new ObjectPool<IcicleProjectile>(transform, _config.IciclePrefab, _config.PoolSize);
        }
    }
    public override void Cast(SpellCastData spellCastData)
    {
        base.Cast(spellCastData);


        float step = 360f / _config.IcicleCount;

        for (int i = 0; i < _config.IcicleCount; i++)
        {
            float angle = step * i;

            Vector2 dir =
                Quaternion.Euler(0, 0, angle) *
                spellCastData.Direction.normalized;

            SpawnIcicle(spellCastData.Position, dir);
        }
    }

    public override void OnEndCast()
    {
        base.OnEndCast();
    }

    public override void SetCooldownTime(float oldCooldownTime, float cooldownTime)
    {
        base.SetCooldownTime(oldCooldownTime, cooldownTime);
    }

    private void SpawnIcicle(Vector2 center, Vector2 direction)
    {
        IcicleProjectile icicle = _pool.GetObject();

        icicle.transform.position = center + direction * _config.Radius;
        bool isCritical;
        float calculatedDamage = DamageCalculator.CalculateDamage(_config.Damage, _config.AttackType, _config.DamageType, _ownerStats, out isCritical);
        icicle.SetPool(_pool);
        icicle.Launch(direction, _config.ProjectileSpeed, calculatedDamage, isCritical, 0.5f);
    }
}
