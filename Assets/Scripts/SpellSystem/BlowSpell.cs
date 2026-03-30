using UnityEngine;

public class BlowSpell :MonoBehaviour, ISpell
{
    [SerializeField] private float _cooldownTimer;
    [SerializeField] private float _cooldownTime;
    
    public void Cast(GameObject caster)
    {
       
    }

    public void ChangeCooldown(float changedAmount)
    {
        
    }

    public GameObject GetOwner()
    {
        return gameObject;
    }
}

