using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField] private GameModeDataSource _gameModeDataSource;
    
    private GameStateMachine _stateMachine;
    
    public GameUi Ui { get; private set; }

    public PowerUpsManager PowerUpsManager { get; private set; }
    public ScoringSystem ScoringSystem { get; private set; }
    public InGameState InGameState { get; private set; }
    public EndGameState EndGameState { get; private set; }
    public TitleGameState HomeState { get; private set; }

    public GameModeData GameModeData => _gameModeDataSource.GameModeData;

    public bool IsPaused { get; private set; }

    public void StartGame()
    {
        _stateMachine.SetState(InGameState);
        IsPaused = false;
    }

    public void GoHome()
    {
        _stateMachine.SetState(HomeState);
    }

    private void Awake()
    {
        Instance = this;

        IsPaused = false;

        InGameState = new InGameState();
        EndGameState = new EndGameState();
        HomeState = new TitleGameState();

        Ui = new GameUi();
        ScoringSystem = new ScoringSystem(this, InventoryManager.Instance.EquipmentSystem);
        PowerUpsManager = new PowerUpsManager(this);
        
        _stateMachine = new GameStateMachine();
        _stateMachine.Initialize(this);

        GameEvents.OnGamePaused += OnPause;
        GameEvents.OnGameUnpaused += OnUnpause;
    }

    private void OnPause()
    {
        IsPaused = true;
    }

    private void OnUnpause()
    {
        IsPaused = false;
    }
    
    private void Start()
    {
        _stateMachine.SetState(HomeState);
    }

    private void Update()
    {
        _stateMachine.Update();
        PowerUpsManager.Update();
    }

    private void OnDestroy()
    {
        _stateMachine?.Dispose();
        GameEvents.OnGamePaused -= OnPause;
        GameEvents.OnGameUnpaused -= OnUnpause;
    }
}
