using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        Entity.Ui.ShowStartGameUi();
    }

    public override void OnExit()
    {
        base.OnExit();
        Entity.Ui.HideStartGameUi();
    }
}
