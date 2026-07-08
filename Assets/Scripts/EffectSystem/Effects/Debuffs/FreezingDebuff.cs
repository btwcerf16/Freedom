using UnityEngine;

public class FreezingDebuff : Effect
{
    private float _tickTimer;
    private const float TickInterval = 1f;
    public override void EffectStart(ActorStats owner)
    {
        base.EffectStart(owner);
        StatModifier statModifier = new(ModifierType.Flat, -((FreezingDebuffData)EffectData).MoveSpeedSlow, this);
        owner.MoveSpeed.AddModifier(statModifier);
    }
    public override void EffectEnd(ActorStats owner) 
    {
        base.EffectEnd(owner);

        //StatModifier statModifier = new(ModifierType.Flat, ((FreezingDebuffData)EffectData).DamagePerSecond, this);
        StatModifier statModifier = new(ModifierType.Flat, ((FreezingDebuffData)EffectData).MoveSpeedSlow, this);
        owner.MoveSpeed.AddModifier(statModifier);
        
    }
    public override void EffectTick(ActorStats owner)
    {
        base.EffectTick(owner);

        TimeRemaining -= Time.deltaTime;
        _tickTimer += Time.deltaTime;

        if (_tickTimer >= TickInterval)
        {
            _tickTimer -= TickInterval;

            owner.SendDamage(((FreezingDebuffData)EffectData).DamagePerSecond, false);
        }
    }
}
