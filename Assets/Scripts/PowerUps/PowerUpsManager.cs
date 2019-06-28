using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpData
{
    public EPowerUpType Type;
    public float TotalTime { get; set; }
    public float Time { get; private set; }
    private bool _active;

    public event Action<PowerUpData> OnActivate;
    public event Action<PowerUpData> OnDeactivate;

    public PowerUpData(EPowerUpType type, float totalTime, Action<PowerUpData> activateCallback, Action<PowerUpData> deactivateCallback)
    {
        TotalTime = totalTime;
        Type = type;
        OnActivate += activateCallback;
        OnDeactivate += deactivateCallback;
    }

    public void Activate()
    {
        if(!_active)
        {
            OnActivate?.Invoke(this);
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
        OnDeactivate?.Invoke(this);
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

public class PowerUpsManager 
{
    public event Action<PowerUpData> OnActivatePowerUp;
    public event Action<PowerUpData> OnDeactivatePowerUp;
    
    private const float _defaultPowerupTime = 5.0f;

    private List<PowerUpData> _powerUps;

    private GameController _gameController;
    
    public PowerUpsManager(GameController gameController)
    {
        _gameController = gameController;
        
        GameEvents.OnPickupPowerUp += OnPickupPowerUp;
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameEnded += OnGameEnded;
        
        _powerUps = new List<PowerUpData>();
        _powerUps.Add(new PowerUpData(EPowerUpType.Star, _defaultPowerupTime, OnActivatePowerUpInternal, OnDeactivatePowerUpInternal));
    }
    
    ~PowerUpsManager()
    {
        GameEvents.OnPickupPowerUp -= OnPickupPowerUp;
        GameEvents.OnGameStarted -= OnGameStarted;
        GameEvents.OnGameEnded -= OnGameEnded;
    }

    private void OnActivatePowerUpInternal(PowerUpData data)
    {
        OnActivatePowerUp?.Invoke(data);
    }

    private void OnDeactivatePowerUpInternal(PowerUpData data)
    {
        OnDeactivatePowerUp?.Invoke(data);
    }

    private void OnGameStarted()
    {
        Reset();
        var starPowerupItem = InventoryManager.Instance.GetItem(InventoryManager.StarPowerupId);
        if (starPowerupItem != null)
        {
            GetPowerUpData(EPowerUpType.Star).TotalTime = starPowerupItem.GetCurrentValue();
        }
    }

    private void OnGameEnded()
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

    public void Update()
    {
        if (_gameController.IsPaused)
        {
            return;
        }
        
        foreach(var powerUp in _powerUps)
        {
            powerUp.Update(Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEvents.NotifyPickupPowerUp(EPowerUpType.Star);
        }
    }
}
