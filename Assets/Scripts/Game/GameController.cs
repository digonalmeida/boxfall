using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public InGameState InGameState = new InGameState();
    public EndGameState EndGameState = new EndGameState();
    public StartGameState StartGameState = new StartGameState();

    private GameStateMachine _stateMachine;

    public int CurrentScore { get; set; }

    [SerializeField]
    private GameUi _ui = null;

    public GameUi Ui
    {
        get { return _ui; }
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
