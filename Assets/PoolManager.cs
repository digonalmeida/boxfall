using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static  PoolManager Instance { get; private set; }

    [SerializeField] 
    private int _bulletInitialSize;
    
    [SerializeField]
    private TurrentDataSource _turrentDataSource;

    [SerializeField] 
    private int _birdsCount;
    
    [SerializeField]
    private SpawnerDataSource _birdSpawnerDataSource;
    
    private Dictionary<GameObject, ObjectPool> _pools = new Dictionary<GameObject, ObjectPool>();

    public void Awake()
    {
        Instance = this;
        InitializePools();
    }

    private void InitializePools()
    {
        InitializePool(_turrentDataSource.TurrentData.BulletPrefab.gameObject, _bulletInitialSize);

        var spawnerInstances = _birdSpawnerDataSource.SpawnerData.SpawningInstances();
        foreach (var spawnerInstance in spawnerInstances)
        {
            InitializePool(spawnerInstance.Prefab, _birdsCount);
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