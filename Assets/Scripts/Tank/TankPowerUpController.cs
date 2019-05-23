using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPowerUpController : TankComponent
{
    public override void Initialize(TankController tank)
    {
        base.Initialize(tank);
        GameEvents.OnActivatePowerUp += OnActivatePowerUp;
        GameEvents.OnDeactivatePowerUp += OnDeactivatePowerUp;
        GameEvents.OnGameStarted += OnGameStarted;
    }

    private void OnDestroy()
    {
        GameEvents.OnActivatePowerUp -= OnActivatePowerUp;
        GameEvents.OnDeactivatePowerUp -= OnDeactivatePowerUp;
        GameEvents.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        Tank.Shield.Deactivate();
    }

    private void OnActivatePowerUp(EPowerUpType type)
    {
        Debug.Log("here");
        if (type != EPowerUpType.Shield)
        {
            return;
        }

        Tank.Shield.Activate();
    }
    
    private void OnDeactivatePowerUp(EPowerUpType type)
    {
        if (type != EPowerUpType.Shield)
        {
            return;
        }

        Tank.Shield.Deactivate();
    }
}
