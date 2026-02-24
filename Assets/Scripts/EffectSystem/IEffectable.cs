using UnityEngine;

public interface IEffectable 
{
    public void PurgeEffect(EffectData effectData);
    public void ApplyEffect(EffectData effectData);

}
