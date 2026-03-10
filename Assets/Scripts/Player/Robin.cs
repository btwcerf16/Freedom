using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robin : PlayableActor, IDamageable
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
        if (PlayerHealthBar != null)
            PlayerHealthBar.SetHealthData(PlayerActorStats.CurrentHealth, PlayerActorStats.CurrentMaxHealth);
    }
    public void GetDamage(float damage, bool isCrit)
    {
        if (PlayerActorStats.CurrentHealth <= damage)
        {
            SceneTransition.SwitchScene("MainMenu");
            gameObject.SetActive(false);

        }
        
        PlayerActorStats.CurrentHealth -= damage;
        if (PlayerHealthBar != null)
        {
            PlayerHealthBar.SetHealthData(PlayerActorStats.CurrentHealth, PlayerActorStats.CurrentMaxHealth);
        }
    }
}
