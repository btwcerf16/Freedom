using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthImage;
    public TextMeshProUGUI HealthText;

    public void SetHealthData(float currentHealth, float maxHealth)
    {
        
        HealthImage.fillAmount = currentHealth/maxHealth;
        HealthText.text = Mathf.RoundToInt(currentHealth) + "/" + maxHealth;
    }
}
