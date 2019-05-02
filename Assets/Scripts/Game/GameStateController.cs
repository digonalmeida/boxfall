using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private GameEntity _gameEntity;
    private void Awake()
    {
        _gameEntity = GetComponent<GameEntity>();
    }

    private void Start()
    {
        _gameEntity.StateMachine = new GameStateMachine();
        _gameEntity.StateMachine.Entity = _gameEntity;
        _gameEntity.StateMachine.SetState(GameEntity.InGameState);
    }

    private void Update()
    {
        _gameEntity.StateMachine.Update(_gameEntity);
    }

    private void OnDestroy()
    {
        _gameEntity.StateMachine?.Dispose();
    }
}
