using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGameState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        Entity.Ui.SetState(EUiState.TitleScreen);
        GameEvents.NotifyEnterTitleState();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
