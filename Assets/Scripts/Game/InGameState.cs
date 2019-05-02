using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : GameState
{
    private GameEntity entity;
    public override void OnEnter(GameEntity entity)
    {
        base.OnEnter(entity);
        entity.CurrentScore = 0;
        GameEvents.OnBirdKilled += OnBirdKilled;
        GameEvents.OnTankDestroyed += OnTankDestroyed;
        this.entity = entity;
        GameEvents.RequestUpdateUI();
        entity.UI.UpdateInGameUI();
    }

    public override void OnExit(GameEntity entity)
    {
        GameEvents.OnBirdKilled -= OnBirdKilled;
        base.OnExit(entity);
    }

    private void OnBirdKilled()
    {
        entity.CurrentScore++;
        GameEvents.RequestUpdateUI();
        entity.UI.UpdateInGameUI();
    }

    private void OnTankDestroyed()
    {
        entity.StateMachine.SetState(GameEntity.EndGameState);
    }
}
