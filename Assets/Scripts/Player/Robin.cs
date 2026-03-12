using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robin : PlayableActor, IDamageable,IDisposable
{
    private List<IDisposable> _disposable = new();

    private void Start()
    {
        PlayerInventory = GetComponent<PlayerInventory>();
        PlayerEffectHandler = GetComponent<EffectHandler>();
        PlayerActorStats = GetComponent<ActorStats>();
        PlayerAnimator = GetComponent<Animator>();

        PlayerCinemachineCamera = GetComponentInChildren<CinemachineCamera>();
        if(PlayerHand != null)
            PlayerHand.Initialize(this);
        IDisposable maxHealthDisposable = PlayerActorStats.CurrentMaxHealth.Subscribe(PlayerHealthBar.SetMaxHealth);
        IDisposable currentHealthDisposable = PlayerActorStats.CurrentHealth.Subscribe(PlayerHealthBar.SetHealthData);
        _disposable.Add(currentHealthDisposable);
        _disposable.Add(maxHealthDisposable);
        
    }
    public void GetDamage(float damage, bool isCrit)
    {
        if (PlayerActorStats.CurrentHealth.Value <= damage)
        {
            SceneTransition.SwitchScene("MainMenu");
            gameObject.SetActive(false);

        }
        
        PlayerActorStats.CurrentHealth.Value -= damage;
    }
    public void GetHeal(float heal)
    {
        PlayerActorStats.CurrentHealth.Value = Mathf.Clamp(PlayerActorStats.CurrentHealth.Value + heal, 0, PlayerActorStats.CurrentMaxHealth.Value);
    }
    public void Dispose()
    {
        foreach(var disp in _disposable)
        {
            disp.Dispose();
        }
    }
}
