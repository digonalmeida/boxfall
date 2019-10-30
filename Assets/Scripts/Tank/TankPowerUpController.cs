using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPowerUpController : TankComponent
{
    private readonly static int _invulnerableAnimationHash;
    [SerializeField]
    private Animator _animator = null;

    private PowerUpsManager _powerUpsManager;
    
    static TankPowerUpController()
    {
        _invulnerableAnimationHash = Animator.StringToHash("Invulnerable");
    }
    
    public bool Invulnerable { get; private set; }
    public override void Initialize(TankController tank)
    {
        base.Initialize(tank);
        _powerUpsManager = GameController.Instance.PowerUpsManager;
    }
    
    protected override void OnGameStarted()
    {
        base.OnGameStarted();
        SetInvulnerable(false);
        
        
        _powerUpsManager.OnActivatePowerUp += OnActivatePowerUp;
        _powerUpsManager.OnDeactivatePowerUp += OnDeactivatePowerUp;
    }
    
    protected override void OnGameEnded()
    {
        base.OnGameEnded();
        SetInvulnerable(false);
        _powerUpsManager.OnActivatePowerUp -= OnActivatePowerUp;
        _powerUpsManager.OnDeactivatePowerUp -= OnDeactivatePowerUp;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _powerUpsManager.OnActivatePowerUp -= OnActivatePowerUp;
        _powerUpsManager.OnDeactivatePowerUp -= OnDeactivatePowerUp;
    }
    

    private void SetInvulnerable(bool invulnerable)
    {
        Invulnerable = invulnerable;
        _animator?.SetBool(_invulnerableAnimationHash, invulnerable);
        Tank.TurrentController.SetInvulnerable(invulnerable);
    }

    private void OnActivatePowerUp(PowerUpData powerUp)
    {
        if (powerUp.Type != EPowerUpType.Star)
        {
            return;
        }

        SetInvulnerable(true);
    }
    
    private void OnDeactivatePowerUp(PowerUpData powerUp)
    {
        if (powerUp.Type != EPowerUpType.Star)
        {
            return;
        }

        SetInvulnerable(false);
    }
}
