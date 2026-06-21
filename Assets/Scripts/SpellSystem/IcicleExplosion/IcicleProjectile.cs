using System.Collections;
using UnityEngine;

public class IcicleProjectile : Projectile, IPoolable<IcicleProjectile>
{
    private ObjectPool<IcicleProjectile> _pool;

    public override void Launch(Vector2 direction, float speed, float damage, bool isCrit, float lifeTime)
    {
        base.Launch(direction, speed, damage, isCrit, lifeTime);
        
        StartCoroutine(returnAfterLaunch(LifeTime));
    }
    public override void OnHit(Collider2D collider2D)
    {
        base.OnHit(collider2D);
        //ReturnIntoPool();
    }
    public void SetPool(ObjectPool<IcicleProjectile> objectPool)
    {
        _pool = objectPool;
    }

    IEnumerator returnAfterLaunch(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        ReturnIntoPool();
        Debug.Log("Вернулся в пул");
    }
    private void ReturnIntoPool()
    {
        Debug.Log("Проверка");
        _pool.ReturnObject(this);
        

    }
}
