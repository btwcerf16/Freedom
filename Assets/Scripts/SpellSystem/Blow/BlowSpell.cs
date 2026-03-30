using UnityEngine;

public class BlowSpell : Spell
{

    public override void Cast()
    {
        base.Cast();
        _cooldownTimer = ((BlowSpellConfig)SpellData).CooldownTime;
        Debug.Log("ÊÀÑÒ ÂÇÐÛÂÀ");

    }
    public void Update()
    {
        if(_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }
}

