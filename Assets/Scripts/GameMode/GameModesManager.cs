using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameModesManager: MonoBehaviour
    {
        public static GameModesManager Instance { get; private set; }

        [SerializeField] private GameModeDataSource _mainGameModeDataSource;
        [SerializeField] private GameModeDataSource _eventGameModeDataSource;
        
        public bool GameModesNotificationBadgeVisible { get; private set; }
        public event Action OnGameModeChange;
        
        public GameModeData MainGameMode { get; set; }
        public GameModeData EventGameMode { get; private set; }

        public bool IsMainGameModeSelected { get; private set; }

        public GameModeData GameModeData => IsMainGameModeSelected ? MainGameMode : EventGameMode;

        private IGameModeDataLoader _eventGameModeLoader;
        public event Action OnEventGameModeReceived;

        private void Awake()
        { 
            Instance = this;
            MainGameMode = _mainGameModeDataSource.GameModeData;
            
            IsMainGameModeSelected = true;
            SetupSourceLocal();
            
        }

        private void OnDestroy()
        {
            _eventGameModeLoader.OnGameModeLoaded -= OnGameModeLoaded;
        }

        private void OnGameModeLoaded()
        {
            GameModesNotificationBadgeVisible = true;
            OnEventGameModeReceived?.Invoke();
        }

        private void Start()
        {
            _eventGameModeLoader.RequestGameModeData();
            UpdateEventGameMode();
            ReloadGameModeScene();
        }
        
        public void ReloadGameModeScene()
        {
            StartCoroutine(LoadGameModeCoroutine());
        }

        public void SetupMainGameMode()
        {
            IsMainGameModeSelected = true;
            OnGameModeChange?.Invoke();
            ReloadGameModeScene();
        }

        public void SetupEventGameMode()
        {
            IsMainGameModeSelected = false;
            OnGameModeChange?.Invoke();
            ReloadGameModeScene();
        }

        public void UpdateEventGameMode()
        {
            GameModeData loadedGameModeData = _eventGameModeLoader.LoadedGameModeData;

            if (EventGameMode == loadedGameModeData)
            {
                return;
            }

            GameModesNotificationBadgeVisible = true;
            
            EventGameMode = loadedGameModeData;
            
            if (!IsMainGameModeSelected && EventGameMode == null)
            {
                SetupMainGameMode();
            }
            else
            {
                SetupEventGameMode();
            }
        }

        public void NotifyEventGameModeSeen()
        {
            GameModesNotificationBadgeVisible = false;
        }

        private IEnumerator LoadGameModeCoroutine()
        {
            GameController.Instance.Ui.SetState(EUiState.Loading);
            string sceneName = "game_mode";
            var scene = SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(sceneName);
            }

            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            GameController.Instance.Ui.UnsetState(EUiState.Loading);
        }

        public void SetupSourceLocal()
        {
            ClearEventGameModeLoader();
            _eventGameModeLoader = new LocalGameModeDataLoader(_eventGameModeDataSource);
            _eventGameModeLoader.OnGameModeLoaded += OnGameModeLoaded;
        }
        
        public void SetupSourceOnline()
        {
            ClearEventGameModeLoader();
            _eventGameModeLoader = new LocalGameModeDataLoader(_eventGameModeDataSource);
            _eventGameModeLoader.OnGameModeLoaded += OnGameModeLoaded;
        }
        
        public void SetupSourceOnlineTest()
        {
            ClearEventGameModeLoader();
            _eventGameModeLoader = new LocalGameModeDataLoader(_eventGameModeDataSource);
            _eventGameModeLoader.OnGameModeLoaded += OnGameModeLoaded;
        }

        private void ClearEventGameModeLoader()
        {
            if (_eventGameModeLoader == null)
            {
                return;
            }
            
            _eventGameModeLoader.OnGameModeLoaded -= OnGameModeLoaded;
        }
    }
}