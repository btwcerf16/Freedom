using UnityEngine;
[CreateAssetMenu(menuName ="ActorStats/Create new actor stats", fileName = "NewActorStats")]
public class BaseActorStats : ScriptableObject
{
    public float MaxHealh;
    public float CritChance;
    public float CritDamageMultiplier;
    public float PiercingDamageMultiplier;
    public float SlashingDamageMultiplier;
    public float BludgeoningDamageMultiplier;
    public float MagicDamageMultiplier;
    public float RangeDamageMultiplier;
    public float MeleeDamageMultiplier;
    public float CooldownReduction;
    
}
