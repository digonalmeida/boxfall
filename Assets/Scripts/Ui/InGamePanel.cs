using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
