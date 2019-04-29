using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class DeadState : State<PlayerCharacterEntity>
{
    public override void OnEnter(PlayerCharacterEntity entity)
    {
        base.OnEnter(entity);
        entity.MovementController.enabled = false;
        entity.TurrentController.enabled = false;
        entity.gameObject.SetActive(false);
    }

    public override void OnExit(PlayerCharacterEntity entity)
    {
        entity.gameObject.SetActive(true);
        base.OnExit(entity);
    }
}
