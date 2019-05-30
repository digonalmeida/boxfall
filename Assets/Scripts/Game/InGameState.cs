using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameEvents.OnTankDestroyed += OnTankDestroyed;
        Entity.Ui.ShowInGameUi();
        GameEvents.NotifyGameStarted();
    }

    public override void OnExit()
    {
        base.OnExit();
        Entity.Ui.HideInGameUi();
    }

    private void OnTankDestroyed()
    {
        ChangeState(Entity.EndGameState);
    }
}
