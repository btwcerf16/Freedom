using UnityEngine;
using UnityEngine.AI;

public class InhibitedDebuff : Effect
{
    public override void EffectEnd(ActorStats owner)
    {
        if (owner.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            agent.acceleration /= ((InhibitedDebuffData)EffectData).InhibitedMultiplier;
            
        }
    }
    public override void EffectTick(ActorStats owner)
    {
        TimeRemaining -= Time.deltaTime;
    }
    public override void EffectStart(ActorStats owner)
    {
       
        if (owner.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            agent.acceleration *= ((InhibitedDebuffData)EffectData).InhibitedMultiplier;
        }
    }

}
