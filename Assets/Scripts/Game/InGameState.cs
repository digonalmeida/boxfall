using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        Entity.CurrentScore = 0;
        GameEvents.OnBirdKilled += OnBirdKilled;
        GameEvents.OnTankDestroyed += OnTankDestroyed;
        Entity.Ui.ShowInGameUi();
        GameEvents.NotifyGameStarted();
    }

    public override void OnExit()
    {
        base.OnExit();
        GameEvents.OnBirdKilled -= OnBirdKilled;
        Entity.Ui.HideInGameUi();
    }

    private void OnBirdKilled()
    {
        Entity.CurrentScore++;
        GameEvents.NotifyScoreChanged();
    }

    private void OnTankDestroyed()
    {
        ChangeState(Entity.EndGameState);
    }
}
