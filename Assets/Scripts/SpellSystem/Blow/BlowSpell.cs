using System.Drawing;
using UnityEditor.Build.Content;
using UnityEngine;

public class BlowSpell : Spell
{

    public float Damage;
    public float Radius;
    public float Cooldown;
    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);
        if (owner == null)
            return;
        var ownerStats = owner.GetComponent<ActorStats>(); 
        Damage = ((BlowSpellConfig)SpellData).BlowDamage * ownerStats.CurrentMagicDamageMultiplier;
        Radius = ((BlowSpellConfig)SpellData).BlowRadius * ownerStats.CurrentMagicDamageMultiplier;
        Cooldown = ((BlowSpellConfig)SpellData).CooldownTime / ownerStats.CurrentCooldownReduction;
    }

    public override void Cast()
    {
        base.Cast();
        if (_cooldownTimer > 0) return;

        _cooldownTimer = Cooldown;
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, Radius);
        foreach (Collider2D target in targets)
        {
            if(target.gameObject ==  _owner)
            {
                target.GetComponent<IDamageable>()?.GetDamage(Damage/2, false);
                Debug.Log("OWNER IS TARGET");
                
            }
            else
            {
                target.GetComponent<IDamageable>()?.GetDamage(Damage, false);
            }
           
        }
        ParticleSystem particle = PoolsController.Instance.BlowSystemPool.GetObject();
        particle.Stop();
        particle.transform.SetParent(null);
        particle.transform.position = transform.position;
        //particle.transform.localScale = transform.localScale;
        //TODO потом пофикси хуйню с тем, что у тебя когда влево игрок смотрит, эффект не видно
        var main = particle.main;

        particle.Play();

    }
    public void Update()
    {
        if(_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }
}

