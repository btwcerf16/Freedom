using System.Drawing;
using UnityEngine;

public class BlowSpell : Spell
{

    public override void Cast()
    {
        base.Cast();
        
        _cooldownTimer = ((BlowSpellConfig)SpellData).CooldownTime;

        ParticleSystem particle = PoolsController.Instance.BlowSystemPool.GetObject();
        particle.Stop();
        particle.transform.SetParent(transform);
        particle.transform.position = transform.position;
        particle.transform.localScale = transform.localScale;

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

