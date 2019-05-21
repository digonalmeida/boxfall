using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class AliveState : State<TankController>
{
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("here");
        Entity.CollisionController.OnHitByTarget += OnHitByTarget;

        Entity.MovementController.enabled = true;
        Entity.TurrentController.enabled = true;
        Entity.CollisionController.enabled = true;
        Entity.Shield.enabled = false;
    }

    private void OnHitByTarget(BirdController birdController)
    {
        birdController.DestroyTarget();
        
        if (Entity.Shield.enabled)
        {
            GameEvents.NotifyBirdKilled();
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
        Entity.Shield.enabled = false;
    }
}
