using UnityEngine;

public interface IPoolable<T> where T :  Component
{
    void SetPool(ObjectPool<T> objectPool);
}
