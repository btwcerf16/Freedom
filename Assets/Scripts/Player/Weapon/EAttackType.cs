using System;
using UnityEngine;
[Flags]
public enum EAttackType
{
    Range = 1 << 0, // 1
    Melee = 1 << 1, // 2
    Magic = 1 << 2  // 4
}
