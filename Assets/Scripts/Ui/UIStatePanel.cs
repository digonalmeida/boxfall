using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIStatePanel : UiElement
{
    private readonly EUiState _uiState;
    private EUiState _previousState = EUiState.None;

    protected UIStatePanel(EUiState uiState)
    {
        _uiState = uiState;
    }
    
    public void Setup(GameUi gameUi)
    {
        gameUi.OnChangeState += OnChangeState;
    }

    protected override void OnDestroy()
    {
        GameController.Instance.Ui.OnChangeState -= OnChangeState;
    }

    public void OnChangeState(EUiState oldState, EUiState newState)
    {
        if (oldState == _uiState)
        {
            _previousState = EUiState.None;
            Hide();
        }
        
        if (newState == _uiState)
        {
            _previousState = oldState;
            Show();
        }
    }

    public void GoBackOrClose()
    {
        if (_previousState == EUiState.None)
        {
            Close();
            return;
        }
        GameController.Instance.Ui.SetState(_previousState);
    }

    public void Close()
    {
        GameController.Instance.Ui.UnsetState(_uiState);
    }
}
