using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect : MonoBehaviour
{
    public float TimeRemaining;

    public EffectData EffectData;

    public void Initialize(EffectData _effectData)
    {
        EffectData = _effectData;
        TimeRemaining = _effectData.EffectDuration;
    }

    public virtual void EffectStart(ActorStats owner) { }
    public virtual void EffectTick(ActorStats owner) { }
    public virtual void EffectEnd(ActorStats owner) { }


}
