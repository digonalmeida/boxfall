using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpData
{
    public EPowerUpType Type;
    public float TotalTime { get; private set; }
    public float Time { get; private set; }
    private bool _active;

    public event Action OnActivate;
    public event Action OnDeactivate;

    public PowerUpData(EPowerUpType type, float totalTime)
    {
        TotalTime = totalTime;
        Type = type;
    }

    public void Activate()
    {
        if(!_active)
        {
            GameEvents.NotifyActivatePowerUp(this);
        }

        _active = true;
        Time = TotalTime;
    }

    public void Deactivate()
    {
        if(!_active)
        {
            return;
        }

        _active = false;
        Time = 0;
        GameEvents.NotifyDeactivatePowerup(this);
    }

    public void Update(float deltaTime)
    {
        if(!_active)
        {
            return;
        }

        Time -= deltaTime;

        if(Time <= 0)
        {
            Deactivate();
        }
    }
}

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField]
    private float _defaultPowerupTime = 5.0f;

    private List<PowerUpData> _powerUps;

    private void Awake()
    {
        GameEvents.OnPickupPowerUp += OnPickupPowerUp;
        _powerUps = new List<PowerUpData>();
        _powerUps.Add(new PowerUpData(EPowerUpType.Shield, _defaultPowerupTime));
    }

    private void OnDestroy()
    {
        GameEvents.OnPickupPowerUp -= OnPickupPowerUp;
    }

    private void OnGameStart()
    {
        Reset();
    }

    private void OnGameEnd()
    {
        Reset();
    }

    private void Reset()
    {
        _powerUps.ForEach(p => p.Deactivate());
    }

    private void OnPickupPowerUp(EPowerUpType type)
    {
        GetPowerUpData(type).Activate();
    }

    private PowerUpData GetPowerUpData(EPowerUpType type)
    {
        return _powerUps.Find(p => p.Type == type);
    }

    private void Update()
    {
        foreach(var powerUp in _powerUps)
        {
            powerUp.Update(Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEvents.NotifyPickupPowerUp(EPowerUpType.Shield);
        }
    }
}
