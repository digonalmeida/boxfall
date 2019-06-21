using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : UIStatePanel
{
    public InGamePanel() 
        : base(EUiState.InGame)
    {
        //
    }
    
    public void Pause()
    {
        GameEvents.NotifyGamePaused();
    }
}
