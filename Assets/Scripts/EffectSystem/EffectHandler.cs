using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EffectDisplay))]
[RequireComponent(typeof(ActorStats))]
public class EffectHandler : MonoBehaviour
{
    public ActorStats OwnerActorStats;

    public List<Effect> ActiveEffects = new();

    public EffectDisplay CharacterEffectDisplay;

    private void Start()
    {
        OwnerActorStats = GetComponent<ActorStats>();
    }
    private void Update()
    {
        for (int i = 0; i < ActiveEffects.Count; i++)
        {

            ActiveEffects[i].EffectTick(OwnerActorStats);

            if (ActiveEffects[i].TimeRemaining <= 0 && ActiveEffects[i].TimeRemaining >= -5)
            {
                ActiveEffects[i].EffectEnd(OwnerActorStats);
                RemoveEffect(ActiveEffects[i]);
            }
        }
    }
    public void AddEffect(EffectData effectData)
    {
        Effect existing = ActiveEffects.Find(effect => effect.EffectData == effectData);
        if (existing != null)
        {
            existing.TimeRemaining = effectData.EffectDuration;
            existing.EffectStart(OwnerActorStats);
            return;

        }
        else
        {
            Effect newEffect = effectData.CreateEffect(gameObject);
            ActiveEffects.Add(newEffect);
            newEffect.EffectStart(OwnerActorStats);
            CharacterEffectDisplay.AddEffectSprite(newEffect);
        }
    }
    public void RemoveEffect(Effect effect)
    {
        ActiveEffects.Remove(effect);
        CharacterEffectDisplay.ClearEffectSprite(effect);
        Destroy(effect);
    }
}


