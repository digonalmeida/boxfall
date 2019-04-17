using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCharacterStateMachine : PlayerCharacterComponent
{
    private StateMachine<PlayerState> _stateMachine;

    private void Start()
    {
        _stateMachine = new StateMachine<PlayerState>();
        _stateMachine.AddTransition(PlayerState.Idle, CheckShootingInput, PlayerState.Shooting);
        _stateMachine.AddTransition(PlayerState.Shooting, () => !PlayerCharacter.Shooting, PlayerState.ComeBack);
        _stateMachine.AddTransition(PlayerState.ComeBack, CheckShootingInput, PlayerState.Shooting);
        _stateMachine.AddTransition(PlayerState.ComeBack, CheckIsCentered, PlayerState.Idle);
        _stateMachine.OnStateChanged += OnStateChangedInternal;
        _stateMachine.CurrentState = PlayerState.Idle;
    }

    private void OnStateChangedInternal(PlayerState state)
    {
        PlayerCharacter.State = state;
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }

    private bool CheckShootingInput()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
    }

    private bool CheckIsCentered()
    {
        return transform.position.x >= 0;
    }
}
