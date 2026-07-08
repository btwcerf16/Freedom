using UnityEngine;

public class HailSpell : Spell
{
    private ObjectPool<RockOfIce> _pool;
    private HailSpellConfig _config => (HailSpellConfig)SpellData;

    public override void Cast(SpellCastData spellCastData)
    {
        base.Cast(spellCastData);
        if (CooldownTimer > 0) return;
        CooldownTimer = CooldownTime;
        for (int i = 0; i < _config.PoolSize; i++)
        {
            Vector2 randomPoint = spellCastData.Position + (Random.insideUnitCircle * _config.Radius);
            RockOfIce rockOfIce = _pool.GetObject();
            rockOfIce.SetPool(_pool);
            rockOfIce.transform.position = randomPoint;
            rockOfIce.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            rockOfIce.transform.localScale = new Vector2(6f,7f);
            rockOfIce.StartFall(_config.Damage, spellCastData.Caster, _ownerStats);
            
        }
        


    }

    public override void SetCooldownTime(float oldCooldownTime, float cooldownTime)
    {
        base.SetCooldownTime(oldCooldownTime, cooldownTime);
    }

    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);
        if (_pool == null)
        {
            _pool = new ObjectPool<RockOfIce>(transform, _config.RockOfIcePrefab, _config.PoolSize);

        }
    }
    public void Update()
    {
        if (CooldownTimer > 0)
        {
            CooldownTimer -= Time.deltaTime;
        }
    }
}
