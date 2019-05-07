using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public static GameEntity Instance { get; private set; }
    public static InGameState InGameState = new InGameState();
    public static EndGameState EndGameState = new EndGameState();
    public static StartGameState StartGameState = new StartGameState();
    
    public GameStateMachine StateMachine {get;set;}
    public int CurrentScore { get; set; }
    

    [SerializeField]
    private GameUi _ui;
    
    public GameStateController StateController { get; private set; }

    public GameUi Ui
    {
        get { return _ui; }
    }

    public void Awake()
    {
        Instance = this;
        StateController = GetComponent<GameStateController>();
    }
}
