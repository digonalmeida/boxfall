using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class PlayerCharacterStateController : PlayerCharacterComponent
{  
    private void Start()
    {
        PlayerCharacter.StateMachine = new StateMachine<PlayerCharacterEntity>();
        PlayerCharacter.StateMachine.Entity = PlayerCharacter;
        SetState(PlayerCharacterEntity.AliveState);
    }

    private void Update()
    {
        PlayerCharacter.StateMachine.Update(PlayerCharacter);
    }

    public void SetState(State<PlayerCharacterEntity> state)
    {
        PlayerCharacter.StateMachine.SetState(state);
    }
}