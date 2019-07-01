using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class IdleState : State<TankController>
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        Entity.MovementController.Reset();
        Entity.MovementController.enabled = true;
        Entity.TurrentController.enabled = false;
        Entity.CollisionController.enabled = false;
        Entity.TankEquipmentController.UpdateEquipment();
    }
}
