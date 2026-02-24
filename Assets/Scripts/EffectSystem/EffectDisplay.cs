using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private List<ParticleSystem> _particleInstances;
    [SerializeField] private List<GameObject> _effectImage;

    public void AddEffectSprite(Effect effect)
    {

        if (_isPlayer)
        {
            GameObject iconPrefab = Instantiate(_effectImagePrefab,_effectPanel.transform);

            
            _effectImage.Add(iconPrefab);
            iconPrefab.GetComponent<EffectIcon>().SetEffectIcon(effect.EffectData.SpriteIcon, effect.EffectData, effect.EffectData.EffectDuration);

        }
        else
        {
            InstanceParticles();
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
            //партиклы удаляет
        }
        

    }
    private void InstanceParticles()
    {
        ParticleSystem particle = Instantiate(_particleSystem, transform);
        particle.startColor = Color.blue;
        _particleInstances.Add(particle);
    }

}
