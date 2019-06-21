using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanel : UIStatePanel
{
    public TitlePanel() 
        : base(EUiState.TitleScreen)
    {
        //
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
        Debug.Log("Leaderboards");
    }

    public void StartGame()
    {
        GameController.Instance.StartGame();
    }
}
