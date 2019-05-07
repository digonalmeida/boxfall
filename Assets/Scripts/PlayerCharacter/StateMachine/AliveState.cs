using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class AliveState : State<PlayerCharacterEntity>
{
    public override void OnEnter(PlayerCharacterEntity entity)
    {
        base.OnEnter(entity);
        entity.MovementController.Enable();
        entity.TurrentController.enabled = true;
        entity.Events.OnHitByABox += OnHitByABox;
        entity.CollisionController.enabled = true;
    }

    private void OnHitByABox(PlayerCharacterEntity entity, Collider2D box)
    {
        entity.StateController.SetState(PlayerCharacterEntity.DeadState);
    }
    
    public override void OnExit(PlayerCharacterEntity entity)
    {
        entity.Events.OnHitByABox -= OnHitByABox;
        entity.MovementController.Disable();
        entity.TurrentController.enabled = false;
        base.OnExit(entity);
    }
}
