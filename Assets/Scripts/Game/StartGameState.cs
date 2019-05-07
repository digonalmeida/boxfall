using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : GameState
{
    public override void OnEnter(GameEntity entity)
    {
        base.OnEnter(entity);
        entity.Ui.ShowStartGameUi();
    }

    public override void OnExit(GameEntity entity)
    {
        base.OnExit(entity);
        entity.Ui.HideStartGameUi();
    }
}
