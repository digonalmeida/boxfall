using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPowerUpController : TankComponent
{
    private readonly static int _invulnerableAnimationHash;
    [SerializeField]
    private Animator _animator = null;

    static TankPowerUpController()
    {
        _invulnerableAnimationHash = Animator.StringToHash("Invulnerable");
    }
    public bool Invulnerable { get; private set; }
    public override void Initialize(TankController tank)
    {
        base.Initialize(tank);
        GameEvents.OnActivatePowerUp += OnActivatePowerUp;
        GameEvents.OnDeactivatePowerUp += OnDeactivatePowerUp;
    }

    private void OnDestroy()
    {
        GameEvents.OnActivatePowerUp -= OnActivatePowerUp;
        GameEvents.OnDeactivatePowerUp -= OnDeactivatePowerUp;
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();
        SetInvulnerable(false);
    }
    
    protected override void OnGameEnded()
    {
        base.OnGameEnded();
        SetInvulnerable(false);
    }

    private void SetInvulnerable(bool invulnerable)
    {
        Invulnerable = invulnerable;
        _animator?.SetBool(_invulnerableAnimationHash, invulnerable);
        Tank.TurrentController.SetInvulnerable(invulnerable);
    }

    private void OnActivatePowerUp(PowerUpData powerUp)
    {
        if (powerUp.Type != EPowerUpType.Shield)
        {
            return;
        }

        SetInvulnerable(true);
    }
    
    private void OnDeactivatePowerUp(PowerUpData powerUp)
    {
        if (powerUp.Type != EPowerUpType.Shield)
        {
            return;
        }

        SetInvulnerable(false);
    }
}
