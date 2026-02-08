using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EffectIcon : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]private Image _image;
    private Image _subImage;
    public EffectData IconEffectData;
    private float _timeRemainig;
    private float _timeDuration;

    private void Awake()
    {
        _subImage = GetComponent<Image>();
        
    }

    private void Update()
    {
        if (_timeRemainig > 0)
        {
            _timeRemainig -= Time.deltaTime;
            _image.fillAmount = Mathf.InverseLerp(0, _timeDuration, _timeRemainig);
        }
    }

    public void SetEffectIcon(Sprite sprite, EffectData effectData, float timeDuration)
    {
        IconEffectData = effectData;    
        _image.sprite = sprite;
        _subImage.sprite = sprite;
        _timeRemainig = timeDuration;
        _timeDuration = timeDuration;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ControlTooltip.Instance.Show(IconEffectData.EffectDescription,transform.position, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ControlTooltip.Instance.Hide(this);
    }
    private void OnDisable()
    {
        ControlTooltip.Instance.Hide(this);
    }
}
