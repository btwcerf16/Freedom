using UnityEngine;

public class ActorStats : MonoBehaviour, IDamageable
{

    [SerializeField] private BaseActorStats _config;
    public float CurrentDamageAttack;
    public float CurrentHealth;
    public float CurrentMaxHealth;
    public float CurrentCritChance;
    public float CurrentCritDamageMultiplier;
    public float CurrentPiercingDamageMultiplier;
    public float CurrentSlashingDamageMultiplier;
    public float CurrentBludgeoningDamageMultiplier;
    public float CurrentMagicDamageMultiplier;
    public float CurrentRangeDamageMultiplier;
    public float CurrentMeleeDamageMultiplier;
    public float CurrentCooldownReduction;

    private void Awake()
    {
        CurrentMaxHealth = _config.MaxHealh;
        CurrentHealth = _config.MaxHealh;
        CurrentCritChance = _config.CritChance;
        CurrentCritChance = _config.CritDamageMultiplier;
        CurrentPiercingDamageMultiplier = _config.PiercingDamageMultiplier;
        CurrentSlashingDamageMultiplier = _config.SlashingDamageMultiplier;
        CurrentBludgeoningDamageMultiplier = _config.BludgeoningDamageMultiplier;
        CurrentMagicDamageMultiplier = _config.MagicDamageMultiplier;
        CurrentRangeDamageMultiplier = _config.RangeDamageMultiplier;
        CurrentMeleeDamageMultiplier = _config.MeleeDamageMultiplier;
        CurrentCooldownReduction = _config.CooldownReduction;
    }
    public void ResetAttackDamage()
    {
        CurrentDamageAttack = 0;
    }
    public void SetAttackDamage(float attackDamage, EAttackType attackType, EDamageType damageType)
    {
        if(attackDamage < 0)
            attackDamage = 0;
        CurrentDamageAttack = attackDamage;
    }
    public void GetDamage(float damage)
    {
        if(damage > CurrentHealth)
        {
            CurrentHealth = 0;
            return;
        }
        
        CurrentHealth -= damage;
    }

}