using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spell : MonoBehaviour, IDisposable
{
    public float CooldownTimer;
    public float CooldownTime;
    public Action<GameObject> OnCast;
    public SpellConfig SpellData;
    [SerializeField] protected GameObject _owner;
    private List<IDisposable> _disposable = new();
    protected ActorStats _ownerStats;

    public void Initialize(SpellConfig spellConfig)
    {
        SpellData = spellConfig;
        
       
    }
    public virtual void Cast()
    {
        OnCast?.Invoke(gameObject);
    }
    public virtual void OnEndCast()
    {
        Debug.Log(gameObject + " Произнес заклинание " + SpellData.SpellName);
    }
    public virtual void SetOwner(GameObject owner)
    {
        _owner = owner;
        _ownerStats = _owner.GetComponent<ActorStats>();
        IDisposable cooldownTimeDisposable = _ownerStats.CurrentCooldownReduction.Subscribe(SetCooldownTime);
        _disposable.Add(cooldownTimeDisposable);
        //CooldownTime = ((BlowSpellConfig)SpellData).CooldownTime / _ownerStats.CurrentCooldownReduction.Value;
    }
    public virtual void SetCooldownTime(float oldCooldownTime, float cooldownTime)
    {
        CooldownTime = SpellData.CooldownTime / _ownerStats.CurrentCooldownReduction.Value;
    }
    public void Dispose()
    {
        foreach (var disp in _disposable)
        {
            disp.Dispose();
        }
    }
}
