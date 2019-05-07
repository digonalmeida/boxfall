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
        entity.Ui.ShowInGameUi();
        GameEvents.NotifyGameStarted();
    }

    public override void OnExit(GameEntity entity)
    {
        GameEvents.OnBirdKilled -= OnBirdKilled;
        entity.Ui.HideInGameUi();
        base.OnExit(entity);
    }

    private void OnBirdKilled()
    {
        GameEntity.Instance.CurrentScore++;
        GameEvents.NotifyScoreChanged();
    }

    private void OnTankDestroyed()
    {
        entity.StateMachine.SetState(GameEntity.EndGameState);
    }
}
