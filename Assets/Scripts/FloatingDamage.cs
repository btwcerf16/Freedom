using TMPro;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    [HideInInspector] public float Damage;
    [SerializeField] public TextMeshPro Text;
   

    private void Start()
    {
        Text.text = "-" + Damage;
    }
}
