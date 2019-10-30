using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameUi
{
    public delegate void ChangeStateDelegate(EUiState oldEUiState, EUiState newEUiState);

    public event ChangeStateDelegate OnChangeState;

    private readonly Dictionary<EUiLayer, EUiState> _currentStates;

    public void SetState(EUiState state)
    {
        EUiLayer layer = GetLayer(state);
        SetState(state, layer);
    }

    private EUiLayer GetLayer(EUiState state)
    {
        switch (state)
        {
            case EUiState.TitleScreen:
            case EUiState.InGame:
            case EUiState.EndGame:
            case EUiState.Shop:
            case EUiState.GameModes:
                return EUiLayer.Base;
            case EUiState.PauseGame:
            case EUiState.ConfirmQuit:
            case EUiState.ConfigPanel:
            case EUiState.Loading:
                return EUiLayer.Popup;
            default:
                return EUiLayer.Base;
        }
    }

    public void UnsetState(EUiState state)
    {
        SetState(EUiState.None, GetLayer(state));
    }
    
    private void SetState(EUiState state, EUiLayer layer)
    {
        //close popups
        foreach (var stateLayer in _currentStates.Keys.ToList())
        {
            if (stateLayer <= layer)
            {
                continue;
            }

            var lastState = _currentStates[stateLayer];
            _currentStates[stateLayer] = EUiState.None;
            OnChangeState?.Invoke(lastState, EUiState.None);
        }
        
        EUiState currentState = _currentStates[layer];
        
        _currentStates[layer] = state;
        
        OnChangeState?.Invoke(currentState, state);
    }
    
    public GameUi()
    {
        _currentStates = new Dictionary<EUiLayer, EUiState>();
        _currentStates[EUiLayer.Base] = EUiState.None;
        _currentStates[EUiLayer.Popup] = EUiState.None;
        _currentStates[EUiLayer.PopupConfirmation] = EUiState.None;
    }
}
