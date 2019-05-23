using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class DeadState : State<TankController>
{
    public override void OnEnter()
    {
        base.OnEnter();
        Entity.ExplosionEffect.SetActive(true);
        Entity.SpriteObject.SetActive(false);
        GameEvents.NotifyTankDestroyed();
    }

    public override void OnExit()
    {
        base.OnExit();
        Entity.ExplosionEffect.SetActive(false);
        Entity.SpriteObject.SetActive(true);
    }
}
