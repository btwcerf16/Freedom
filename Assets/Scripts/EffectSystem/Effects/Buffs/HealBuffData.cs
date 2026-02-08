using UnityEngine;
[CreateAssetMenu(menuName = "Effects/Buffs/HealData", fileName = " HealData")]
public class HealBuffData : EffectData
{
    public float HealCount;
    public override Effect CreateEffect(GameObject owner)
    {
        Effect effect = owner.AddComponent<HealBuff>();

        effect.Initialize(this);
        return effect;
    }
}
