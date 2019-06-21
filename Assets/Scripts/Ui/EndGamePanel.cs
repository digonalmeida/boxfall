using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : UIStatePanel
{
    public EndGamePanel() 
        : base(EUiState.EndGame)
    {
        //
    }
    
    public void GoHome()
    {
        GameController.Instance.GoHome();
    }

    public void PlayAgain()
    {
        GameController.Instance.StartGame();
    }
}
