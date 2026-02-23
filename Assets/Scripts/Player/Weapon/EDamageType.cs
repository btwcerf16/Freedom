using System;
using UnityEngine;
[Flags]
public enum EDamageType
{
    Piercing = 1 << 0, // 1
    Slashing = 1 << 1, // 2
    Bludgeoning = 1 << 2  // 4
}
