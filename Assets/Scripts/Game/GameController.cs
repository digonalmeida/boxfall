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

    [SerializeField]
    private LevelController _levelController;
    
    private GameStateMachine _stateMachine;
    
    public GameUi Ui
    {
        get { return _ui; }
    }

    public PowerUpsManager PowerUpsManager
    {
        get { return _powerUpsManager; }
    }

    public LevelController LevelController
    {
        get { return _levelController; }
    }

    public InGameState InGameState { get; private set; }
    public EndGameState EndGameState { get; private set; }
    public StartGameState HomeState { get; private set; }

    public void StartGame()
    {
        _stateMachine.SetState(InGameState);
    }

    public void GoHome()
    {
        _stateMachine.SetState(HomeState);
    }

    private void Awake()
    {
        Instance = this;

        InGameState = new InGameState();
        EndGameState = new EndGameState();
        HomeState = new StartGameState();

        _stateMachine = new GameStateMachine();
        _stateMachine.Initialize(this);
    }

    private void Start()
    {
        _stateMachine.SetState(HomeState);
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
