using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : GameState
{
    public override void OnEnter(GameEntity entity)
    {
        base.OnEnter(entity);
        entity.UI.UpdateEndGameUI();
    }
}
