using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class AliveState : State<TankController>
{
    public override void OnEnter()
    {
        base.OnEnter();
        Entity.CollisionController.OnHitByTarget += OnHitByTarget;

        Entity.MovementController.Reset();
        Entity.MovementController.enabled = true;
        Entity.TurrentController.enabled = true;
        Entity.CollisionController.enabled = true;
    }

    private void OnHitByTarget(BirdController birdController)
    {
        if (Entity.TankPowerUpController.Invulnerable)
        {
            birdController.KillBird();
            return;
        }
        
        ChangeState(Entity.DeadState);
    }
    
    public override void OnExit()
    {
        base.OnExit();
        Entity.CollisionController.OnHitByTarget -= OnHitByTarget;

        Entity.MovementController.enabled = false;
        Entity.TurrentController.enabled = false;
        Entity.CollisionController.enabled = false;
    }
}
