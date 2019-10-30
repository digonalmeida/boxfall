using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiElement: MonoBehaviour
{
    private bool _initialized = false;
    private List<Action> _updateEvents = new List<Action>();

    public void CheckInitialized()
    {
        if (_initialized)
        {
            return;
        }

        Initialize();
        _initialized = true;
    }

    protected virtual void Initialize()
    {
        //
    }
    
    public void Show()
    {
        if (gameObject == null)
        {
            return;
        }
        
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (gameObject == null)
        {
            return;
        }
        
        gameObject.SetActive(false);
    }

    public virtual void OnShow()
    {
        CheckInitialized();
    }

    public virtual void OnHide()
    {
    }

    private void OnEnable()
    {
        OnShow();
    }

    private void OnDisable()
    {
        OnHide();
    }

    protected virtual void OnDestroy()
    {
        Hide();
    }
}
