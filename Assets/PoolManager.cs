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
    
    private Dictionary<GameObject, ObjectPool> _pools = new Dictionary<GameObject, ObjectPool>();

    public void Awake()
    {
        Instance = this;
        InitializePools();
    }

    private void InitializePools()
    {
        InitializePool(_turrentDataSource.TurrentData.BulletPrefab.gameObject, _bulletInitialSize);
    }

    private void InitializePool(GameObject prefab, int initialSize)
    {
        var pool = new GameObject("ObjectPool_" + prefab.name, typeof(ObjectPool)).GetComponent<ObjectPool>();
        pool.transform.parent = transform;
        pool.Initialize(prefab, initialSize);
        _pools[prefab] = pool;
    }

    public GameObject GetInstance(GameObject prefab)
    {
        var pool = _pools[prefab];
        if (pool == null)
        {
            return Instantiate(prefab);
        }

        return pool.GetInstance();
    }
}

[SerializeField]
public class PoolManagerConfig
{
    
}

[SerializeField]
public class PoolConfig
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField] 
    private int _initialCount;

    public GameObject Prefab => _prefab;
    public int InitialCount => _initialCount;
}