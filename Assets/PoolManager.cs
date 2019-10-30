using System;
using System.Collections.Generic;
using Birds;
using DefaultNamespace;
using SpawnerV2;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static  PoolManager Instance { get; private set; }

    [SerializeField] 
    private int _bulletInitialSize;

    [SerializeField]
    private int _spawnableInitialSize = 10;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private GameObject _powerupPrefab;
    
    private Dictionary<int, ObjectPool> _birdPools = new Dictionary<int, ObjectPool>();
    private ObjectPool _powerupPool;
    private ObjectPool _bulletPool;
    
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
        TurrentData turrentData = GameModesManager.Instance.GameModeData.TurrentData;
        BirdData[] birdDatas = GameModesManager.Instance.GameModeData.Birds;
        
        _bulletPool = InitializePool(_bulletPrefab, _bulletInitialSize);
        _powerupPool = InitializePool(_powerupPrefab, _spawnableInitialSize);

        for (int i = 0; i < birdDatas.Length; i++)
        {
            InitializeBirdPool(i, birdDatas[i]);
        }
    }

    private ObjectPool InitializePool(GameObject prefab, int initialSize)
    {        
        var pool = new GameObject("ObjectPool_" + prefab.name, typeof(ObjectPool)).GetComponent<ObjectPool>();
        pool.transform.parent = transform;
        pool.Initialize(prefab, initialSize);
        return pool;
    }

    public GameObject GetInstance(SpawningInstance spawningInstance)
    {
        if (spawningInstance.SpawnType == SpawningInstance.ESpawnType.Bird)
        {
            return GetBird(spawningInstance.SpawnId);
        }
        else
        {
            return _powerupPool.GetInstance();
        }
    }

    public GameObject GetBullet()
    {
        return _bulletPool.GetInstance();
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