using UnityEngine;
[CreateAssetMenu(menuName = "Effects/Debuffs/FreezingData", fileName = "FreezingData")]
public class FreezingDebuffData : EffectData
{
    public float MoveSpeedSlow;
    public float DamagePerSecond;
    public override Effect CreateEffect(GameObject owner)
    {
        Effect effect = owner.AddComponent<FreezingDebuff>();

        effect.Initialize(this);
        return effect;
    }
}
