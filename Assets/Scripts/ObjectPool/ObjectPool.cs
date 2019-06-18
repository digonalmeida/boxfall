using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private PoolableObject _prefab;

    [SerializeField]
    private int _initialSize = 10;

    [SerializeField]
    private Stack<PoolableObject> _instances = new Stack<PoolableObject>(10);

    private static Dictionary<PoolableObject, ObjectPool> _pools = new Dictionary<PoolableObject, ObjectPool>();

    public static ObjectPool GetPool(PoolableObject prefab)
    {
        ObjectPool instance;
        _pools.TryGetValue(prefab, out instance);
        return instance;
    }

    public void Awake()
    {
        if(_prefab == null)
        {
            return;
        }

        _pools[_prefab] = this;
        for(int i = 0; i < _initialSize; i++)
        {
            Instantiate();
        }
    }

    private void Instantiate()
    {
        var instance = Instantiate(_prefab, transform).GetComponent<PoolableObject>();
        instance.gameObject.SetActive(false);
        _instances.Push(instance);
    }

    private void OnInstanceHidden(PoolableObject instance)
    {
        _instances.Push(instance);
        instance.OnRecycle -= OnInstanceHidden;
    }

    public GameObject GetInstance()
    {
        PoolableObject instance;
        if(_instances.Count == 0)
        {
            Instantiate();
        }

        instance = _instances.Pop();

        instance.OnRecycle += OnInstanceHidden;
        instance.Show();
        return instance.gameObject;
    }
}
