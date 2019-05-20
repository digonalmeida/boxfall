using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class DeadState : State<TankController>
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameEvents.NotifyTankDestroyed();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
