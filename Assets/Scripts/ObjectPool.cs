using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component, IPoolable
{
    private readonly T prefab;
    private readonly Transform parent;
    private readonly Queue<T> pool = new Queue<T>();

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            var obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        T obj = pool.Count > 0 ? pool.Dequeue() : GameObject.Instantiate(prefab, parent);
        obj.gameObject.SetActive(true);
        obj.OnSpawnedFromPool();
        return obj;
    }

    public void Return(T obj)
    {
        obj.OnReturnedToPool();
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}

