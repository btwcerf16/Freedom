using System.Drawing;
using UnityEditor.Build.Content;
using UnityEngine;

public class BlowSpell : Spell
{

    public float BlowDamage;
    public float BlowRadius;
    
    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);
        if (owner == null)
            return;
        var ownerStats = owner.GetComponent<ActorStats>();
        BlowDamage = ((BlowSpellConfig)SpellData).BlowDamage * ownerStats.CurrentMagicDamageMultiplier;
        BlowRadius = ((BlowSpellConfig)SpellData).BlowRadius * ownerStats.CurrentMagicDamageMultiplier;
        
    }

    public override void Cast()
    {
        base.Cast();
        if (CooldownTimer > 0) return;

        CooldownTimer = CooldownTime;
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, BlowRadius);
        foreach (Collider2D target in targets)
        {
            if(target.gameObject ==  _owner)
            {
                target.GetComponent<IDamageable>()?.GetDamage(BlowDamage / 5, false);
                Debug.Log("OWNER IS TARGET");
                
            }
            else
            {
                target.GetComponent<IDamageable>()?.GetDamage(BlowDamage, false);
            }
           
        }
        ParticleSystem particle = PoolsController.Instance.BlowSystemPool.GetObject();
        particle.Stop();
        particle.transform.SetParent(null);
        particle.transform.position = transform.position;
        //particle.transform.localScale = transform.localScale;
        
        var main = particle.main;

        particle.Play();
        OnEndCast();
    }
    public override void OnEndCast()
    {
        base.OnEndCast();

    }
    public void Update()
    {
        if(CooldownTimer > 0)
        {
            CooldownTimer -= Time.deltaTime;
        }
    }
}

