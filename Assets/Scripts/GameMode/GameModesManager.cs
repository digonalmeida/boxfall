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


        public event Action OnGameModeChange;
        public GameModeData MainGameMode { get; set; }
        public GameModeData EventGameMode { get; private set; }
        
        public bool IsMainGameModeSelected { get; private set; }

        public GameModeData CurrentGameModeData => IsMainGameModeSelected ? MainGameMode : EventGameMode;
        
        private void Awake()
        { 
            Instance = this;
            MainGameMode = _mainGameModeDataSource.GameModeData;
            EventGameMode = _eventGameModeDataSource.GameModeData;
            IsMainGameModeSelected = true;

            StartCoroutine(LoadGameModeCoroutine());
        }

        private void Start()
        {
            ReloadGameModeScene();
        }

        public void ReloadGameModeScene()
        {
            StartCoroutine(LoadGameModeCoroutine());
        }

        public void SetMainGameMode()
        {
            IsMainGameModeSelected = true;
            NotifyGameModeChange();
        }

        public void SetEventGameMode()
        {
            if (!IsMainGameModeSelected || EventGameMode == null)
            {
                return;
            }
            IsMainGameModeSelected = false;
            NotifyGameModeChange();
        }

        private void NotifyGameModeChange()
        {
            OnGameModeChange?.Invoke();
            ReloadGameModeScene();
        }

        public void CheckEventGameMode()
        {
            EventGameMode = _eventGameModeDataSource != null ? _eventGameModeDataSource.GameModeData : null;
            
            if (!IsMainGameModeSelected && EventGameMode == null)
            {
                SetMainGameMode();
            }
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
    }
}