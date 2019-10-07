using System;
using System.Collections.Generic;
using Birds;
using SpawnerV2;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static  PoolManager Instance { get; private set; }

    [SerializeField] 
    private int _bulletInitialSize;

    [SerializeField] private int _spawnableInitialSize = 10;

    private Dictionary<GameObject, ObjectPool> _pools = new Dictionary<GameObject, ObjectPool>();
    
    private Dictionary<int, ObjectPool> _birdPools = new Dictionary<int, ObjectPool>();
    
    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        TurrentData turrentData = GameController.Instance.GameModeData.TurrentData;
        BirdData[] birdDatas = GameController.Instance.GameModeData.Birds;
        
        InitializePool(turrentData.BulletPrefab.gameObject, _bulletInitialSize);

        for (int i = 0; i < birdDatas.Length; i++)
        {
            InitializeBirdPool(i, birdDatas[i]);
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

    public GameObject GetInstance(SpawningInstance spawningInstance)
    {
        if (spawningInstance.SpawnType == SpawningInstance.ESpawnType.Bird)
        {
            return GetBird(spawningInstance.SpawnId);
        }
        else
        {
            var pool = _pools[spawningInstance.Prefab];
            return pool.GetInstance();
        }
    }
    
    public GameObject GetInstance(GameObject prefab)
    {
       var pool = _pools[prefab];
        
        return pool.GetInstance();
    }

    public GameObject GetBird(int instanceId)
    {
        if(!_birdPools.TryGetValue(instanceId, out ObjectPool pool))
        {
            return null;
        }

        return pool.GetInstance();
    }

    private void InitializeBirdPool(int id, BirdData birdData)
    {
        var pool = new GameObject("bird_pool_" + birdData.Name, typeof(BirdObjectPool)).GetComponent<BirdObjectPool>();
        pool.transform.parent = transform;
        pool.Initialize(birdData, _spawnableInitialSize);
        _birdPools[id] = pool;
    }
}