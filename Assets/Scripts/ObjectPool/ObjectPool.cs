using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private PoolableObject _prefab;

    [SerializeField]
    private Stack<PoolableObject> _instances = new Stack<PoolableObject>(10);

    public void Initialize(GameObject prefab, int initialSize)
    {
        if (prefab != null)
        {
            _prefab = prefab.GetComponent<PoolableObject>();
        }

        for(int i = 0; i < initialSize; i++)
        {
            AddInstance(Instantiate());
        }
    }

    protected virtual PoolableObject Instantiate()
    {
        PoolableObject instance = Instantiate(_prefab);
        return instance;
    }

    private void AddInstance(PoolableObject instance)
    {
        instance.transform.parent = transform;
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
