using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurrentController : TankComponent
{
    [SerializeField]
    private TurrentController _turrent = null;

    public event Action OnShot;

    private void Update()
    {
        if (IsPaused)
        {
            return;
        }
        
        if(!enabled)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
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
