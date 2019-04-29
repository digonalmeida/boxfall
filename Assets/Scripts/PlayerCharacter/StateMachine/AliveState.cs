using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class AliveState : State<PlayerCharacterEntity>
{
    public override void OnEnter(PlayerCharacterEntity entity)
    {
        base.OnEnter(entity);
        entity.MovementController.enabled = true;
        entity.TurrentController.enabled = true;
        entity.Events.OnHitByABox += OnHitByABox;
    }

    private void OnHitByABox(PlayerCharacterEntity entity, Collider2D box)
    {
        entity.StateController.SetState(PlayerCharacterEntity.DeadState);
    }
    
    public override void OnExit(PlayerCharacterEntity entity)
    {
        entity.Events.OnHitByABox -= OnHitByABox;
        entity.MovementController.enabled = false;
        entity.TurrentController.enabled = false;
        base.OnExit(entity);
    }
}
