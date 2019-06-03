using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameEvents.NotifyShowHomeScreen();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
