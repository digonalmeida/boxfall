using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class GameModesPanel : UIStatePanel
{
    [SerializeField] private Button _mainModeButton;
    [SerializeField] private Button _mainModeSelected;
    [SerializeField] private Button _eventModeButton;
    [SerializeField] private Button _eventModeSelected;
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
        _gameModesManager.SetupMainGameMode();
    }

    public void SetEventGameMode()
    {
        _gameModesManager.SetupEventGameMode();
    }

    public override void OnShow()
    {
        base.OnShow();
        _gameModesManager = GameModesManager.Instance;
        _gameModesManager.OnGameModeChange += UpdateUi;
        _gameModesManager.OnEventGameModeReceived += OnEventGameModeReceived;
        UpdateUi();
        GameModesManager.Instance.NotifyEventGameModeSeen();
    }

    private void OnEventGameModeReceived()
    {
        _gameModesManager.UpdateEventGameMode();
    }

    public override void OnHide()
    {
        base.OnHide();
        _gameModesManager.OnGameModeChange -= UpdateUi;
        _gameModesManager.OnEventGameModeReceived -= OnEventGameModeReceived;
        GameModesManager.Instance.NotifyEventGameModeSeen();
    }
    
    private void UpdateUi()
    {
        _mainModeButton.gameObject.SetActive(!_gameModesManager.IsMainGameModeSelected);
        _mainModeSelected.gameObject.SetActive(_gameModesManager.IsMainGameModeSelected);
        
        GameModeData eventGameMode = _gameModesManager.EventGameMode;
        if (eventGameMode == null)
        {
            SetupNoEventGameModePanel();
            return;
        }

        _eventModeButton.gameObject.SetActive(_gameModesManager.IsMainGameModeSelected);
        _eventModeSelected.gameObject.SetActive(!_gameModesManager.IsMainGameModeSelected);
        _eventModeTitle.text = eventGameMode.Name;
        _eventModeDescription.text = eventGameMode.Description;
    }

    private void SetupNoEventGameModePanel()
    {
        _eventModeButton.gameObject.SetActive(false);
        _eventModeSelected.gameObject.SetActive(false);
        _eventModeTitle.text = "No Event";
        _eventModeDescription.text = "No active game mode event.";
    }
}
