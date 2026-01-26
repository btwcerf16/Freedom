using System;
using UnityEngine;
[Flags]
public enum EDamageType
{
    Piercing = 0, // Пронзающий
    Slashing = 1,//Рубящий
    Bludgeoning = 2, //Дробящий
    Fire = 3,
    Cold = 4,
    Lightning = 5,
    Poison = 6,
    Acid = 7
}
