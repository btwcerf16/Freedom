using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour
{
     public List<SpellConfig> SpellConfigs;
     public List<Spell> Spells;
     
     public void CastSpell(int index, SpellCastData spellCastData)
     {
        Spells[index].Cast(spellCastData);
     }

    private void Start()
    {
        foreach (SpellConfig spellConfig in SpellConfigs)
        {
            Spells.Add(spellConfig.AddSpell(gameObject));
        }
    }
    public void ReplaceSpell(int index, SpellConfig spellConfig)
    {
        SpellConfigs[index] = spellConfig;
        Spells[index] = spellConfig.AddSpell(gameObject);
    } 
    public void ResetSpellCooldown(int index)
    {
        Spells[index].CooldownTimer = 0;
    }
}
