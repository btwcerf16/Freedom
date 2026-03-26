using System;
using UnityEngine;

public abstract class Spell
{
    public float Cooldown;
    public float CooldownTimer;
    public bool IsPassive = false;
    public Action OnSpellUsed;
    public bool IsSpelling;

    public virtual void StartCast() { }
    public virtual void Cast() { }
    public virtual void EndCast() { }

}
