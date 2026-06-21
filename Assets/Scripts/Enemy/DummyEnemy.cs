using UnityEngine;

public class DummyEnemy : Enemy, IDamageable
{
    private void Start()
    {
        EnemyStateMachine = new StateMachine();
    }
    public void Update()
    {

        EnemyStateMachine.CurrentState?.Update();

    }
    public void GetDamage(float damage, bool isCrit)
    {
        if (damage >= EnemyStats.CurrentHealth.Value)
        {
            Debug.Log("онлеп");
            
            IsDead = true;
        }
        //(Instantiate(_floatingDamage, damagePos, Quaternion.identity)).GetComponent<FloatingDamage>();
        Vector2 damagePos = new Vector2(transform.position.x + .5f, transform.position.y + 1.0f);
        FloatingDamage floatingDamage = PoolsController.Instance.DamageTextPool.GetObject();
        floatingDamage.transform.position = damagePos;

        floatingDamage.Damage = damage;
        if (isCrit)
            floatingDamage.Text.color = Color.darkRed;
        else
            floatingDamage.Text.color = Color.whiteSmoke;
        EnemyStats.CurrentHealth.Value -= damage;
    }

    public void GetHeal(float heal)
    {
        EnemyStats.CurrentHealth.Value = Mathf.Clamp(EnemyStats.CurrentHealth.Value + heal, 0, EnemyStats.MaxHealth.CurrentValue);
    }
}
