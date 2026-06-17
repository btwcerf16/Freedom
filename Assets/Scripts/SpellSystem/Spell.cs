using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spell : MonoBehaviour, IDisposable
{
    public float CooldownTimer;
    public float CooldownTime;
    public event Action<SpellCastData> OnCast;
    public SpellConfig SpellData;
    [SerializeField] protected GameObject _owner;
    private List<IDisposable> _disposable = new();
    protected ActorStats _ownerStats;

    public void Initialize(SpellConfig spellConfig)
    {
        SpellData = spellConfig;
    }
    public virtual void Cast(SpellCastData spellCastData)
    {
        OnCast?.Invoke(spellCastData);
        Debug.Log(OnCast);
    }
    public virtual void OnEndCast()
    {
        Debug.Log(gameObject + " Произнес заклинание " + SpellData.SpellName);
    }
    public virtual void SetOwner(GameObject owner)
    {
        _owner = owner;
        _ownerStats = _owner.GetComponent<ActorStats>();
        IDisposable cooldownTimeDisposable = _ownerStats.CooldownReduction.Value.Subscribe(SetCooldownTime);
        _disposable.Add(cooldownTimeDisposable);
        
    }
    public virtual void SetCooldownTime(float oldCooldownTime, float cooldownTime)
    {
        CooldownTime = SpellData.CooldownTime / _ownerStats.CooldownReduction.CurrentValue;
    }
    public void Dispose()
    {
        foreach (var disp in _disposable)
        {
            disp.Dispose();
        }
    }
}
