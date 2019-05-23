using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    class PowerUpData
    {
        public float Time;
    }
    
    [SerializeField]
    private float _defaultPowerupTime = 5.0f;
    
    private Dictionary<EPowerUpType, PowerUpData> _powerUpTime = new Dictionary<EPowerUpType, PowerUpData>();

    private void Awake()
    {
        GameEvents.OnPickupPowerUp += OnPickupPowerUp;
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
        foreach (var type in _powerUpTime.Keys)
        {
            _powerUpTime[type].Time = 0;
        }
    }

    private void OnPickupPowerUp(EPowerUpType type)
    {
        ActivatePowerUp(type);
    }

    private void ActivatePowerUp(EPowerUpType type)
    {
        if (!CheckIsActive(type))
        {
            GameEvents.NotifyActivatePowerUp(type);
        }

        if (!_powerUpTime.ContainsKey(type))
        {
            _powerUpTime.Add(type, new PowerUpData());
        }
        
        _powerUpTime[type].Time = _defaultPowerupTime;
    }

    private void DeactivatePowerUp(EPowerUpType type)
    {
        _powerUpTime[type].Time = 0;
        GameEvents.NotifyDeactivatePowerup(type);
    }

    private bool CheckIsActive(EPowerUpType type)
    {
        PowerUpData data;
        if(!_powerUpTime.TryGetValue(type, out data))
        {
            return false;
        }

        return data.Time > 0;
    }

    private void Update()
    {
        foreach (var type in _powerUpTime.Keys.ToList())
        {
            var value = _powerUpTime[type];
            
            if (value.Time <= 0)
            {
                continue;
            }
            
            value.Time -= Time.deltaTime;

            _powerUpTime[type] = value;
            
            if (value.Time <= 0)
            {
                DeactivatePowerUp(type);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEvents.NotifyPickupPowerUp(EPowerUpType.Shield);
        }
    }
}
