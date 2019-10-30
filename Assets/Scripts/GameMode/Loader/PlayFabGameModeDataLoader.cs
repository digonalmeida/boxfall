using System;
using System.Collections;
using UnityEngine;

public class PlayFabGameModeDataLoader : IGameModeDataLoader
{
    private const string _currentEventPropertyName = "current_event";
    private const string _testEventPropertyName = "current_event_test";
            
    public GameModeData LoadedGameModeData => _loadedGameModeData;
    public event Action OnGameModeLoaded;
    
    private GameModeData _loadedGameModeData;
    
    private string _sourcePropetyName;
    
    public PlayFabGameModeDataLoader()
    {
        _sourcePropetyName = _currentEventPropertyName;
    }

    public void SetupTestingSource()
    {
        _sourcePropetyName = _testEventPropertyName;
    }
    
    public void RequestGameModeData()
    {
        PlayFabManager.Instance.OnDataUpdated += OnDataUpdated;
        PlayFabManager.Instance.RequestTitleData();
    }

    private void OnDataUpdated()
    {
        string jsonValue = PlayFabManager.Instance.GetTitleData(_sourcePropetyName);
        if (string.IsNullOrEmpty(jsonValue))
        {
            return;
        }

        _loadedGameModeData = JsonUtility.FromJson<GameModeData>(jsonValue);
        OnGameModeLoaded?.Invoke();
    }
}
