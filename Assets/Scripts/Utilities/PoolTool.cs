using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolTool : MonoBehaviour
{
    public GameObject objPrefab;
    
    private ObjectPool<GameObject> pool;

    private void Start()
    {
        // Initialize the pool
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(objPrefab, transform),
            actionOnGet:(obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
        
        // Pre-fill the pool
        // PreFillPool(10);
    }
    
    // private void PreFillPool(int count)
    // {
    //     var objects = new GameObject[count];
    //     for (int i = 0; i < count; i++)
    //     {
    //         objects[i]=pool.Get();
    //     }
    //
    //     for (int i = 0; i < count; i++)
    //     {
    //         pool.Release(objects[i]);
    //     }
    // }
    
    public GameObject GetObjectFromPool()
    {
        return pool.Get();
    }
    
    public void ReleaseObjectIntoPool(GameObject obj)
    {
        pool.Release(obj);
    }
}
