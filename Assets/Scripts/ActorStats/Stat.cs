using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public float BaseValue;
    private readonly List<StatModifier> _modifiers = new();

    public ReactiveVariable<float> Value = new();
    public float CurrentValue => Value.Value;
    public void Initialize(float baseValue)
    {
        BaseValue = baseValue;
        ClearModifiers();
        Recalculate();
    }

    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);
        Recalculate();
    }

    public void RemoveModifier(StatModifier modifier)
    {
        _modifiers.Remove(modifier);
        Recalculate();
    }
    public void ClearModifiers()
    {
        _modifiers.Clear();
        Recalculate();
    }
    private void Recalculate()
    {
        float result = BaseValue;

        foreach (var modifier in _modifiers)
        {
            if (modifier.Type == ModifierType.Flat)
            {
                result += modifier.Value;
            }
        }

        foreach (var modifier in _modifiers)
        {
            if (modifier.Type == ModifierType.Percent)
            {
                result *= modifier.Value;
            }
        }

        Value.Value = result;
    }

}
