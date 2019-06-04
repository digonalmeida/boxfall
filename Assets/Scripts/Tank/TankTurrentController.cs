using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurrentController : TankComponent
{
    [SerializeField]
    private TurrentController _turrent = null;

    public event Action OnShot;

    protected override void OnGameStarted()
    {
        base.OnGameStarted();
        GameEvents.OnBackgroundClicked += OnBackgroundClicked;
    }

    protected override void OnGameEnded()
    {
        base.OnGameEnded();
        GameEvents.OnBackgroundClicked -= OnBackgroundClicked;
    }

    public void OnBackgroundClicked()
    {
        if (IsPaused)
        {
            return;
        }
        
        if(!enabled)
        {
            return;
        }

        Shoot();
    }
    
    public void SetInvulnerable(bool invulnerable)
    {
        _turrent.SetInvulnerable(invulnerable);
    }

    private void Shoot()
    {
        _turrent.Fire();
        OnShot?.Invoke();
    }
}
