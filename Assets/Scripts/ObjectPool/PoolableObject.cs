using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public event Action<PoolableObject> OnRecycle;
    public event Action OnShow;

    [SerializeField] private Component[] _managedComponents;

    public void Show()
    {
        OnShow?.Invoke();
        SetManagedComponentsEnabled(true);
    }

    public void Recycle()
    {
        OnRecycle?.Invoke(this);
        SetManagedComponentsEnabled(false);
    }
    
    private void SetManagedComponentsEnabled(bool enabledValue)
    {
        if (_managedComponents == null)
        {
            return;
        }

        foreach (var managedComponent in _managedComponents)
        {
            if (managedComponent is MonoBehaviour monoBehaviour)
            {
                monoBehaviour.enabled = enabledValue;
            }

            if (managedComponent is Renderer renderer)
            {
                renderer.enabled = enabledValue;
            }
        }
    }
}
