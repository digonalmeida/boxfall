using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        SocialSystem.Instance.SendScore(Entity.ScoringSystem.CurrentScore);
    }
}
