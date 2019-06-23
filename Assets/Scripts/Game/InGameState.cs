using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameEvents.OnTankDestroyed += OnTankDestroyed;
        GameEvents.NotifyGameStarted();
        Entity.Ui.SetState(EUiState.InGame);
    }

    public override void OnExit()
    {
        base.OnExit();
        GameEvents.NotifyGameUnpaused();
        GameEvents.NotifyGameEnded();
        Entity.Ui.SetState(EUiState.EndGame);
    }

    private void OnTankDestroyed()
    {
        ChangeState(Entity.EndGameState);
    }
}
