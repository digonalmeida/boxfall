using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public GameStateMachine StateMachine {get;set;}
    public int CurrentScore { get; set; }
    public static InGameState InGameState = new InGameState();
    public static EndGameState EndGameState = new EndGameState();

    public GameStateController StateController { get; private set; }
    public GameUI UI { get; private set; }

    public void Awake()
    {
        StateController = GetComponent<GameStateController>();
        UI = GetComponent<GameUI>();
    }
}
