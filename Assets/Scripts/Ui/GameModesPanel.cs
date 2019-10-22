using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class GameModesPanel : UIStatePanel
{
    [SerializeField] private Button _mainModeButton;
    [SerializeField] private Button _eventModeButton;
    [SerializeField] private Text _eventModeTitle;
    [SerializeField] private Text _eventModeDescription;

    private GameModesManager _gameModesManager;
    
    public GameModesPanel() 
        : base(EUiState.GameModes)
    {
        //
    }
    
    public void SetMainGameMode()
    {
        _gameModesManager.SetMainGameMode();
    }

    public void SetEventGameMode()
    {
        _gameModesManager.SetEventGameMode();
    }

    public override void OnShow()
    {
        base.OnShow();
        _gameModesManager = GameModesManager.Instance;
        _gameModesManager.OnGameModeChange += UpdateUi;
        UpdateUi();

    }
    public override void OnHide()
    {
        base.OnHide();
        _gameModesManager.OnGameModeChange -= UpdateUi;
    }
    
    private void UpdateUi()
    {
        _mainModeButton.interactable = !_gameModesManager.IsMainGameModeSelected;
        GameModeData eventGameMode = _gameModesManager.EventGameMode;
        if (eventGameMode == null)
        {
            SetupNoEventGameModePanel();
            return;
        }

        _eventModeButton.interactable = _gameModesManager.IsMainGameModeSelected;
        _eventModeTitle.text = eventGameMode.Name;
        _eventModeDescription.text = eventGameMode.Description;
    }

    private void SetupNoEventGameModePanel()
    {
        _eventModeButton.interactable = false;
        _eventModeTitle.text = "No Event";
        _eventModeDescription.text = "No active game mode event.";
    }
}
