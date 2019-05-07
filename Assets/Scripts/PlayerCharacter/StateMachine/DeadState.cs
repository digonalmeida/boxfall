﻿using System.Collections;
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
        entity.ExplosionEffect.SetActive(true);
        entity.CollisionController.SetEnabled(false);
        entity.SpriteObject.SetActive(false);
        GameEvents.NotifyTankDestroyed();
    }

    public override void OnExit(PlayerCharacterEntity entity)
    {
        entity.gameObject.SetActive(true);
        entity.MovementController.enabled = true;
        entity.TurrentController.enabled = true;
        entity.ExplosionEffect.SetActive(false);
        entity.CollisionController.SetEnabled(true);
        entity.SpriteObject.SetActive(true);
        base.OnExit(entity);
    }
}
