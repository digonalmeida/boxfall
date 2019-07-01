using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedPanel : UIStatePanel
{
    public PausedPanel() 
        : base(EUiState.PauseGame)
    {
        //
    }
    
    public void Unpause()
    {
        GameController.Instance.Ui.UnsetState(EUiState.PauseGame);
        GameEvents.NotifyGameUnpaused();
    }

    public void GoHome()
    {
        GameController.Instance.Ui.SetState(EUiState.ConfirmQuit);
    }

    public void Config()
    {
        GameController.Instance.Ui.SetState(EUiState.ConfigPanel);
    }
}
