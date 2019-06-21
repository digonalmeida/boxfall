using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    
    [SerializeField] 
    private PowerUpsManager _powerUpsManager;

    private GameStateMachine _stateMachine;
    
    public GameUi Ui { get; private set; }

    public PowerUpsManager PowerUpsManager
    {
        get { return _powerUpsManager; }
    }

    public ScoringSystem ScoringSystem { get; private set; }
    public InGameState InGameState { get; private set; }
    public EndGameState EndGameState { get; private set; }
    public TitleGameState HomeState { get; private set; }

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
        HomeState = new TitleGameState();

        ScoringSystem = new ScoringSystem(this);
        Ui = new GameUi();
        
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
