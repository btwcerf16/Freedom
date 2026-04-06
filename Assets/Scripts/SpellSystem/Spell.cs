using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected float _cooldownTimer;
    public Action<GameObject> OnCast;
    public SpellConfig SpellData;
    [SerializeField] protected GameObject _owner;
    public void Initialize(SpellConfig spellConfig)
    {
        SpellData = spellConfig;
    }
    public virtual void Cast()
    {
        Debug.Log(gameObject + " Произнес заклинание " +  SpellData.SpellName);
        OnCast?.Invoke(gameObject);
    }
    public virtual void SetOwner(GameObject owner)
    {
        _owner = owner;
    }
}
