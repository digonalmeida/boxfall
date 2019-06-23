using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : UIStatePanel
{
    [SerializeField]
    private Image _powerupBar = null;

    [SerializeField]
    private Text _currenciesText = null;

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
