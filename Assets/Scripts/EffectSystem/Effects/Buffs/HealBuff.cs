using UnityEngine;

public class HealBuff : Effect
{
    public override void EffectStart(ActorStats owner)
    {
        Debug.Log("Хилл" + ((HealBuffData)EffectData).HealCount);
        owner.CurrentHealth += ((HealBuffData)EffectData).HealCount;
    }
    public override void EffectEnd(ActorStats owner)
    {
       
    }

    public override void EffectTick(ActorStats owner)
    {
        Debug.Log("Хилл" + ((HealBuffData)EffectData).HealCount * Time.deltaTime);
        TimeRemaining -= Time.deltaTime;
    }
}
