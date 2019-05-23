using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField]
    private GameUi _ui = null;

    [SerializeField] 
    private PowerUpsManager _powerUpsManager;
    
    private GameStateMachine _stateMachine;
    private readonly InGameState _inGameState = new InGameState();
    private readonly EndGameState _endGameState = new EndGameState();
    private readonly StartGameState _startGameState = new StartGameState();

    public int CurrentScore { get; set; }

    public GameUi Ui
    {
        get { return _ui; }
    }

    public PowerUpsManager PowerUpsManager
    {
        get { return _powerUpsManager; }
    }

    public InGameState InGameState
    {
        get { return _inGameState; }
    }

    public EndGameState EndGameState
    {
        get { return _endGameState; }
    }

    public StartGameState StartGameState
    {
        get { return _startGameState; }
    }

    public void StartGame()
    {
        _stateMachine.SetState(InGameState);
    }

    private void Awake()
    {
        Instance = this;
        _stateMachine = new GameStateMachine();
        _stateMachine.Initialize(this);
    }

    private void Start()
    {
        _stateMachine.SetState(StartGameState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void OnDestroy()
    {
        _stateMachine?.Dispose();
    }
}
