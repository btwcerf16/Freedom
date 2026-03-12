using UnityEngine;

public interface IDamageable
{
    void GetDamage(float damage, bool isCrit);
    void GetHeal(float heal);
}
