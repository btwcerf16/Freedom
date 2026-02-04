using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public void GetDamage(float Damage)
    {
        Debug.Log("Получил урон " + Damage);
    }
}
