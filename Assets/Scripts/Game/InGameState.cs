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
    }

    public override void OnExit()
    {
        base.OnExit();
        GameEvents.NotifyGameUnpaused();
        GameEvents.NotifyGameEnded();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameEvents.NotifyGamePaused();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            GameEvents.NotifyGameUnpaused();
        }
    }

    private void OnTankDestroyed()
    {
        ChangeState(Entity.EndGameState);
    }
}
