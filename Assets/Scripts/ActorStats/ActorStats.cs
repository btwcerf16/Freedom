using UnityEngine;

public class ActorStats : MonoBehaviour
{
    [SerializeField] private BaseActorStats _config;

    public ReactiveVariable<float> CurrentHealth = new();

    public float VisibleCurrentHealth => CurrentHealth.Value;
    public float VisibleMaxHealth => MaxHealth.CurrentValue;

    public Stat MaxHealth = new();
    public Stat AttackDamage = new();

    public Stat CritChance = new();
    public Stat CritDamageMultiplier = new();

    public Stat PiercingDamageMultiplier = new();
    public Stat SlashingDamageMultiplier = new();
    public Stat BludgeoningDamageMultiplier = new();
    public Stat MagicDamageMultiplier = new();

    public Stat RangeDamageMultiplier = new();
    public Stat MeleeDamageMultiplier = new();

    public Stat CooldownReduction = new();
    public Stat MoveSpeed = new();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        MaxHealth.Initialize(_config.MaxHealh);
        CurrentHealth.Value = MaxHealth.CurrentValue;

        AttackDamage.Initialize(_config.AttackDamage);

        CritChance.Initialize(_config.CritChance);
        CritDamageMultiplier.Initialize(_config.CritDamageMultiplier);

        PiercingDamageMultiplier.Initialize(_config.PiercingDamageMultiplier);
        SlashingDamageMultiplier.Initialize(_config.SlashingDamageMultiplier);
        BludgeoningDamageMultiplier.Initialize(_config.BludgeoningDamageMultiplier);
        MagicDamageMultiplier.Initialize(_config.MagicDamageMultiplier);

        RangeDamageMultiplier.Initialize(_config.RangeDamageMultiplier);
        MeleeDamageMultiplier.Initialize(_config.MeleeDamageMultiplier);

        CooldownReduction.Initialize(_config.CooldownReduction);

        MoveSpeed.Initialize(_config.MoveSpeed);
    }

    public void ResetAttackDamage()
    {
        AttackDamage.Initialize(0);
    }

    public void SetAttackDamage(float attackDamage,
        EAttackType attackType,
        EDamageType damageType)
    {
        if (attackDamage < 0)
            attackDamage = 0;

        AttackDamage.Initialize(attackDamage);
    }
}