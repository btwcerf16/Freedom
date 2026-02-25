using Unity.Cinemachine;
using UnityEngine;

public abstract class PlayableActor : MonoBehaviour
{
    public Animator PlayerAnimator;
    public Hand PlayerHand;
    public PlayerInventory PlayerInventory;
    public EffectHandler PlayerEffectHandler;
    public ActorStats PlayerActorStats;
    public CinemachineCamera PlayerCinemachineCamera;   
}
