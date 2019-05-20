using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurrentController : TankComponent
{
    [SerializeField]
    private TurrentController _turrent;

    public event Action OnShot;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    
    private void Shoot()
    {
        _turrent.Fire();
        OnShot?.Invoke();
    }
}
