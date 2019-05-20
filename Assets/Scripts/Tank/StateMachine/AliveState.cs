using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class AliveState : State<TankController>
{
    public override void OnEnter()
    {
        base.OnEnter();
        Entity.MovementController.Enable();
        Entity.TurrentController.enabled = true;
        Entity.CollisionController.OnHitByTarget += OnHitByTarget;
        Entity.CollisionController.enabled = true;
    }

    private void OnHitByTarget(Target target)
    {
        ChangeState(TankController.DeadState);
    }
    
    public override void OnExit()
    {
        base.OnExit();
        Entity.CollisionController.OnHitByTarget -= OnHitByTarget;
        Entity.MovementController.Disable();
        Entity.TurrentController.enabled = false;
    }
}
