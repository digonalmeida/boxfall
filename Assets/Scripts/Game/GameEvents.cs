using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnTankDestroyed;
    public static event Action OnBirdKilled;
    public static event Action OnShotFired;
    public static event Action OnGameStarted;
    public static event Action OnGameEnded;
    public static event Action OnGamePaused;
    public static event Action OnGameUnpaused;
    public static event Action OnEnterTitleState;
    
    public static event Action OnBirdSpawned;
    public static event Action OnUiAccept;
    public static event Action<EPowerUpType> OnPickupPowerUp;
    public static event Action<PowerUpData> OnActivatePowerUp;
    public static event Action<PowerUpData> OnDeactivatePowerUp;
    public static event Action OnBackgroundClicked;
    
    
    public static void NotifyTankDestroyed()
    {
        OnTankDestroyed?.Invoke();
    }

    public static void NotifyBirdKilled()
    {
        OnBirdKilled?.Invoke();
    }

    public static void NotifyShotFired()
    {
        OnShotFired?.Invoke();
    }

    public static void NotifyGameStarted()
    {
        OnGameStarted?.Invoke();
    }

    public static void NotifyGameEnded()
    {
        OnGameEnded?.Invoke();
    }
    
    public static void NotifyBirdSpawned()
    {
        OnBirdSpawned?.Invoke();
    }

    public static void NotifyUiAccept()
    {
        OnUiAccept?.Invoke();
    }

    public static void NotifyPickupPowerUp(EPowerUpType type)
    {
        OnPickupPowerUp?.Invoke(type);
    }
    
    public static void NotifyActivatePowerUp(PowerUpData data)
    {
        OnActivatePowerUp?.Invoke(data);
    }
    
    public static void NotifyDeactivatePowerup(PowerUpData data)
    {
        OnDeactivatePowerUp?.Invoke(data);
    }

    public static void NotifyGamePaused()
    {
        OnGamePaused?.Invoke();
        GameController.Instance.Ui.SetState(EUiState.PauseGame);
    }

    public static void NotifyGameUnpaused()
    {
        OnGameUnpaused?.Invoke();
        GameController.Instance.Ui.SetState(EUiState.None);
    }

    public static void NotifyBackgroundClicked()
    {
        OnBackgroundClicked?.Invoke();
    }

    public static void NotifyEnterTitleState()
    {
        OnEnterTitleState?.Invoke();
    }
}
