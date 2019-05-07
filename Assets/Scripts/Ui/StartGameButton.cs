using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : UiButton
{
    public override void OnClick()
    {
        base.OnClick();
        GameEntity.Instance.StateMachine.SetState(GameEntity.InGameState);
    }
}
