using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EffectData : ScriptableObject
{
    public string EffectName;
    public float EffectDuration;
    [TextArea] public string EffectDescription;
    public Sprite SpriteIcon;

    public abstract Effect CreateEffect(GameObject owner);

}
