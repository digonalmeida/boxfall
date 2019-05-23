using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPowerUpController : BirdComponent
{
    protected override void Awake()
    {
        base.Awake();
        Bird.OnKilled += OnKilled;
    }

    private void OnDestroy()
    {
        Bird.OnKilled -= OnKilled;
    }

    private void OnKilled()
    {
        GameEvents.NotifyPickupPowerUp(EPowerUpType.Shield);
    }
}