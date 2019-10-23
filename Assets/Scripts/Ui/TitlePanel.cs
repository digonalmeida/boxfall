using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TitlePanel : UIStatePanel
{
    [SerializeField] 
    private GameObject gameModesNotificationBadge;
    public TitlePanel() 
        : base(EUiState.TitleScreen)
    {
        //
    }

    public override void OnShow()
    {
        base.OnShow();
        GameModesManager.Instance.OnEventGameModeReceived += UpdateEvent;
        UpdateEvent();
    }

    public override void OnHide()
    {
        base.OnHide();
        GameModesManager.Instance.OnEventGameModeReceived -= UpdateEvent;
    }

    private void UpdateEvent()
    {
        GameModesManager.Instance.UpdateEventGameMode();
        gameModesNotificationBadge.SetActive(GameModesManager.Instance.GameModesNotificationBadgeVisible);
    }

    public void ShowShop()
    {
        GameController.Instance.Ui.SetState(EUiState.Shop);
    }
    
    public void Config()
    {
        GameController.Instance.Ui.SetState(EUiState.ConfigPanel);
    }

    public void Leaderboards()
    {
        SocialSystem.Instance.ShowLeaderboards();
    }

    public void StartGame()
    {
        GameController.Instance.StartGame();
    }

    public void ChangeMode()
    {
        GameController.Instance.Ui.SetState(EUiState.GameModes);
    }
}
