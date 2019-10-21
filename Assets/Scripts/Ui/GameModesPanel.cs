using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModesPanel : UIStatePanel
{
    [SerializeField] private Button _mainModeButton;
    [SerializeField] private Button _eventModeButton;
    [SerializeField] private Text _eventModeTitle;
    [SerializeField] private Text _eventModeDescription;
    
    public GameModesPanel() 
        : base(EUiState.GameModes)
    {
        //
    }
    
    public void SetMainGameMode()
    {
        GameController.Instance.SetMainGameMode();
    }

    public void SetEventGameMode()
    {
        GameController.Instance.SetEventGameMode();
    }

    public override void OnShow()
    {
        base.OnShow();
    }
}
