using UnityEngine;
using UnityEngine.Android;

public class ActorStats : MonoBehaviour
{

    [SerializeField] private BaseActorStats _config;
    public float CurrentDamageAttack;
    public ReactiveVariable<float> CurrentMaxHealth = new();
    public ReactiveVariable<float> CurrentHealth = new();
    public float _visibleCurrentHealth => CurrentHealth.Value;
    public float _visibleMaxHealth => CurrentMaxHealth.Value;
    public float CurrentCritChance;
    public float CurrentCritDamageMultiplier;
    public float CurrentPiercingDamageMultiplier;
    public float CurrentSlashingDamageMultiplier;
    public float CurrentBludgeoningDamageMultiplier;
    public float CurrentMagicDamageMultiplier;
    public float CurrentRangeDamageMultiplier;
    public float CurrentMeleeDamageMultiplier;
    public float CurrentCooldownReduction;

    private void Initialize()
    {

        CurrentMaxHealth.Value = _config.MaxHealh;
        CurrentHealth.Value = _config.MaxHealh;
        CurrentCritChance = _config.CritChance;
        CurrentCritDamageMultiplier = _config.CritDamageMultiplier;
        CurrentPiercingDamageMultiplier = _config.PiercingDamageMultiplier;
        CurrentSlashingDamageMultiplier = _config.SlashingDamageMultiplier;
        CurrentBludgeoningDamageMultiplier = _config.BludgeoningDamageMultiplier;
        CurrentMagicDamageMultiplier = _config.MagicDamageMultiplier;
        CurrentRangeDamageMultiplier = _config.RangeDamageMultiplier;
        CurrentMeleeDamageMultiplier = _config.MeleeDamageMultiplier;
        CurrentCooldownReduction = _config.CooldownReduction;
        CurrentDamageAttack = _config.AttackDamage;
    }
    private void Start()
    {
        Initialize();


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


}