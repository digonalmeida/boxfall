using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public event Action<PoolableObject> OnRecycle;
    public event Action OnShow;

    public void Show()
    {
        OnShow?.Invoke();
    }

    public void Recycle()
    {
        OnRecycle?.Invoke(this);
    }
}
