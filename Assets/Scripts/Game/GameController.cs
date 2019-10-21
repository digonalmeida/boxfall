using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField] private GameModeDataSource _gameModeDataSource;
    [SerializeField] private GameModeDataSource _eventModeDataSource;
    [SerializeField]
    private bool _isMainGameMode;
    
    private GameStateMachine _stateMachine;
    
    public GameUi Ui { get; private set; }

    public PowerUpsManager PowerUpsManager { get; private set; }
    public ScoringSystem ScoringSystem { get; private set; }
    public InGameState InGameState { get; private set; }
    public EndGameState EndGameState { get; private set; }
    public TitleGameState HomeState { get; private set; }

    public GameModeData GameModeData
    {
        get
        {
            return _isMainGameMode
                ? _gameModeDataSource.GameModeData
                : _eventModeDataSource.GameModeData;
        }
    }

    public bool IsPaused { get; private set; }

    public void StartGame()
    {
        _stateMachine.SetState(InGameState);
        IsPaused = false;
    }
    
    private IEnumerator LoadGameModeCoroutine()
    {
        Ui.SetState(EUiState.Loading);
        string sceneName = "game_mode";
        var scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(sceneName);
        }

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Ui.UnsetState(EUiState.Loading);
    }

    public void ChangeMode()
    {
        _isMainGameMode = !_isMainGameMode;
        
        StartCoroutine(LoadGameModeCoroutine());
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
        
        StartCoroutine(LoadGameModeCoroutine());
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

    public void SetMainGameMode()
    {
        _isMainGameMode = true;
        StartCoroutine(LoadGameModeCoroutine());
    }
    
    public void SetEventGameMode()
    {
        _isMainGameMode = false;
        StartCoroutine(LoadGameModeCoroutine());
    }
}
