using UnityEngine;
[CreateAssetMenu(menuName = "Effects/Debuffs/InhibitedData", fileName = "InhibitedData")]
public class InhibitedDebuffData : EffectData
{
    public float InhibitedMultiplier;
    public override Effect CreateEffect(GameObject owner)
    {
        Effect effect = owner.AddComponent<InhibitedDebuff>();

        effect.Initialize(this);
        return effect;
    }
}
