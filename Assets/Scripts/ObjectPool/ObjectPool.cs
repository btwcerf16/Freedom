using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private Transform _parent;
    private T _prefab;
    private Queue<T> _pool = new Queue<T>();

    public ObjectPool(Transform parent, T prefab, int size)
    {
        _parent = parent;
        _prefab = prefab;

        for (int i = 0; i < size; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject()
    {
        T obj = Component.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
        return obj;
    }
    public T GetObject()
    {
        if(_pool.Count == 0)
            CreateObject();
        T obj = _pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.parent = _parent;
        _pool.Enqueue(obj);
    }
}
