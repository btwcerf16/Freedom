using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour, IDisposable
{
    public float CooldownTimer;
    public float CooldownTime;

    public event Action<SpellCastData> OnCast;

    public SpellConfig SpellData;

    [SerializeField] protected GameObject _owner;
    protected ActorStats _ownerStats;

    private readonly List<IDisposable> _disposable = new();
    private IDisposable _cooldownSubscription;

    public void Initialize(SpellConfig spellConfig)
    {
        SpellData = spellConfig;
    }

    public virtual void Cast(SpellCastData spellCastData)
    {
        OnCast?.Invoke(spellCastData);
    }

    public virtual void SetOwner(GameObject owner)
    {
        _cooldownSubscription?.Dispose();
        _cooldownSubscription = null;

        _owner = owner;

        if (_owner == null)
        {
            _ownerStats = null;
            return;
        }

        _ownerStats = _owner.GetComponent<ActorStats>();

        _cooldownSubscription =
            _ownerStats.CooldownReduction.Value.Subscribe(SetCooldownTime);

        _disposable.Add(_cooldownSubscription);
    }

    public virtual void SetCooldownTime(float oldCooldownTime, float cooldownTime)
    {
        CooldownTime = SpellData.CooldownTime /
                       _ownerStats.CooldownReduction.CurrentValue;
    }

    public virtual void OnEndCast()
    {
        Debug.Log($"{gameObject} cast {SpellData.SpellName}");
    }
    public LayerMask GetProjectileLayerMask()
    {
        if (_owner.CompareTag("Player"))
            return LayerMask.GetMask("Enemy", "Walls");

        if (_owner.CompareTag("Enemy"))
            return LayerMask.GetMask("Player", "Walls");

        return LayerMask.GetMask("Walls");
    }
    public void Dispose()
    {
        foreach (var disp in _disposable)
        {
            disp.Dispose();
        }
    }
}