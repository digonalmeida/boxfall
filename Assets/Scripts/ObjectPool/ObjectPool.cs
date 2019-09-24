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

    
    public void Initialize(GameObject prefab, int initialSize)
    {
        _prefab = prefab.GetComponent<PoolableObject>();
        if (_prefab == null)
        {
            Debug.LogError("pool object is not poolable");
            return;
        }
        _initialSize = initialSize;
        
        for(int i = 0; i < _initialSize; i++)
        {
            Instantiate();
        }
    }

    private void Instantiate()
    {
        PoolableObject instance = Instantiate(_prefab, transform);
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
