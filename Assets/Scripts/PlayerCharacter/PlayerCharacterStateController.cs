using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class PlayerCharacterStateController : PlayerCharacterComponent
{
    private void Awake()
    {
        base.Awake();
        PlayerCharacter.StateMachine = new StateMachine<PlayerCharacterEntity>();
        PlayerCharacter.StateMachine.Entity = PlayerCharacter;
    }
    
    private void Start()
    {
        gameObject.SetActive(false);
        GameEvents.OnGameStarted += OnGame;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= OnGame;
    }

    private void OnGame()
    {
        gameObject.SetActive(true);
        SetState(PlayerCharacterEntity.AliveState);
    }

    private void Update()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        
        PlayerCharacter.StateMachine.Update(PlayerCharacter);
    }

    public void SetState(State<PlayerCharacterEntity> state)
    {
        PlayerCharacter.StateMachine.SetState(state);
    }
}