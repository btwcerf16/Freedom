using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthImage;
    public TextMeshProUGUI HealthText;
    private float _maxHealth;
    public void SetMaxHealth(float oldMaxHealth, float newMaxHealth)
    {
        _maxHealth = newMaxHealth;
    }
    public void SetHealthData(float oldHealth, float currentHealth)
    {
        
        HealthImage.fillAmount = currentHealth/ _maxHealth;
        HealthText.text = Mathf.RoundToInt(currentHealth) + "/" + _maxHealth;
    }
}
