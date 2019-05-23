using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        Entity.Ui.ShowEndGameUi();
        GameEvents.NotifyGameEnded();
    }

    public override void OnExit()
    {
        base.OnExit();
        Entity.Ui.HideEndGameUi();
    }
}
