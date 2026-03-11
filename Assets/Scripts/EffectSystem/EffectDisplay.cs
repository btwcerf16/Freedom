using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ActorStats))]
public class EffectDisplay : MonoBehaviour
{
    [SerializeField] private bool _isPlayer;
    [SerializeField] private GameObject _effectPanel;
    [SerializeField] private GameObject _effectImagePrefab;
    [SerializeField] private ParticleSystem _particleSystem;
    private Dictionary<Effect,ParticleSystem> _particleInstances = new();
    [SerializeField] private List<GameObject> _effectImage;

    public void AddEffectSprite(Effect effect, Action deathAction = null)
    {
        deathAction += OnDestroyClear;
        if (_isPlayer)
        {
            GameObject iconPrefab = Instantiate(_effectImagePrefab,_effectPanel.transform);

            
            _effectImage.Add(iconPrefab);
            iconPrefab.GetComponent<EffectIcon>().SetEffectIcon(effect.EffectData.SpriteIcon, effect.EffectData, effect.EffectData.EffectDuration);

        }
        else
        {
            InstanceParticles(effect.EffectData.EffectColor, effect.EffectData.EffectDuration, effect);
        }
    }
    public void ClearEffectSprite(Effect effect)
    {
        if (_isPlayer)
        {
            for (int i = 0; i < _effectImage.Count; i++)
            {
                if (_effectImage[i].GetComponent<Image>().sprite == effect.EffectData.SpriteIcon)
                {

                    GameObject cleanable = _effectImage[i];
                    _effectImage.Remove(_effectImage[i]);
                    Destroy(cleanable);
                }
            }
        }
        else
        {

            if (_particleInstances.TryGetValue(effect, out var particle))
            {
                particle.Stop();
                PoolsController.Instance.ParticleSystemPool.ReturnObject(particle);
                _particleInstances.Remove(effect);

            }
        }


    }
    private void OnDestroyClear()
    {
        foreach(var instance in _particleInstances)
        {
            instance.Value.Stop();
            PoolsController.Instance.ParticleSystemPool.ReturnObject(instance.Value);
            
        }
    }
    private void InstanceParticles(Color32 color, float duration, Effect effect)
    {
        ParticleSystem particle = PoolsController.Instance.ParticleSystemPool.GetObject();

        particle.Stop();

        particle.transform.SetParent(transform);
        particle.transform.position = transform.position;
        particle.transform.localScale = transform.localScale;

        var main = particle.main;
        main.duration = duration;
        main.startColor = new ParticleSystem.MinMaxGradient(color);

        particle.Play();

        _particleInstances.Add(effect, particle);
    }

}
