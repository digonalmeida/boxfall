using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceFactory
{
    private static ServiceFactory _instance = null;
    public static ServiceFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServiceFactory();
            }

            return _instance;
        }
    }
    
    private Dictionary<Type, object> _instances = new Dictionary<Type, object>();

    public void Register<T>(T instance)
    {
        _instances[typeof(T)] = instance;
    }

    public void Reset()
    {
        foreach (var instance in _instances)
        {
            if (instance.Value is IDisposable)
            {
                ((IDisposable)instance.Value).Dispose();
            }
        }
        _instances.Clear();
    }
}
