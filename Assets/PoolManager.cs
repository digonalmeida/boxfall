using System;
using System.Collections.Generic;
using SpawnerV2;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static  PoolManager Instance { get; private set; }

    [SerializeField] 
    private int _bulletInitialSize;

    [SerializeField] private int _spawnableInitialSize = 10;

    private Dictionary<GameObject, ObjectPool> _pools = new Dictionary<GameObject, ObjectPool>();
    
    public void Awake()
    {
        Instance = this;
    }
    
    public void Initialize(TurrentData turrentData, SpawnerData[] spawners)
    {
        InitializePool(turrentData.BulletPrefab.gameObject, _bulletInitialSize);
        
        foreach (var spawnerData in spawners)
        {
            List<SpawningInstance> spawnerInstances = spawnerData.SpawningInstances();
            foreach (var spawnerInstance in spawnerInstances)
            {
                InitializePool(spawnerInstance.Prefab, _spawnableInitialSize);
            }
        }
    }

    private void InitializePool(GameObject prefab, int initialSize)
    {
        if (_pools.ContainsKey(prefab))
        {
            return;
        }
        
        var pool = new GameObject("ObjectPool_" + prefab.name, typeof(ObjectPool)).GetComponent<ObjectPool>();
        pool.transform.parent = transform;
        pool.Initialize(prefab, initialSize);
        _pools[prefab] = pool;
    }

    public GameObject GetInstance(GameObject prefab)
    {
        var pool = _pools[prefab];

        return pool.GetInstance();
    }
}