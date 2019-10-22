using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TitlePanel : UIStatePanel
{
    public TitlePanel() 
        : base(EUiState.TitleScreen)
    {
        //
    }

    public override void OnShow()
    {
        GameModesManager.Instance.CheckEventGameMode();
        base.OnShow();
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
