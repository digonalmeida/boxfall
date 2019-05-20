using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class DeadState : State<TankController>
{
    public override void OnEnter()
    {
        base.OnEnter();
        Entity.MovementController.enabled = false;
        Entity.TurrentController.enabled = false;
        Entity.ExplosionEffect.SetActive(true);
        Entity.CollisionController.SetEnabled(false);
        Entity.SpriteObject.SetActive(false);
        GameEvents.NotifyTankDestroyed();
    }

    public override void OnExit()
    {
        base.OnExit();
        Entity.gameObject.SetActive(true);
        Entity.MovementController.enabled = true;
        Entity.TurrentController.enabled = true;
        Entity.ExplosionEffect.SetActive(false);
        Entity.CollisionController.SetEnabled(true);
        Entity.SpriteObject.SetActive(true);
    }
}
