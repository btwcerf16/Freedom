using Unity.Cinemachine;
using UnityEngine;

public class Robin : PlayableActor
{
    private void Awake()
    {
        PlayerInventory = GetComponent<PlayerInventory>();
        PlayerEffectHandler = GetComponent<EffectHandler>();
        PlayerActorStats = GetComponent<ActorStats>();
        PlayerAnimator = GetComponent<Animator>();

        PlayerCinemachineCamera = GetComponentInChildren<CinemachineCamera>();
        if(PlayerHand != null)
            PlayerHand.Initialize(this);
    }
}
