using UnityEngine;

public class HealBuff : Effect
{
    public override void EffectEnd(ActorStats owner)
    {
       
    }

    public override void EffectTick(ActorStats owner)
    {
        //Debug.Log("Хилл" + ((HealBuffData)EffectData).HealCount * Time.deltaTime);
        owner.GetComponent<IDamageable>().GetHeal(((HealBuffData)EffectData).HealCount * Time.deltaTime);
        TimeRemaining -= Time.deltaTime;
    }
}
