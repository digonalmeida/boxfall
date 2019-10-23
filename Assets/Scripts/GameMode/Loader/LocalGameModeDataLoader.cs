using System;
using System.Collections;
using UnityEngine;

public class LocalGameModeDataLoader : IGameModeDataLoader
{
    private readonly GameModeDataSource _gameModeDataSource;
    public GameModeData LoadedGameModeData => _loadedGameModeData;
    public event Action OnGameModeLoaded;
    
    private GameModeData _loadedGameModeData;
    public LocalGameModeDataLoader(GameModeDataSource gameModeDataSource)
    {
        _gameModeDataSource = gameModeDataSource;
    }
    
    public void RequestGameModeData()
    {
        EventProxy.Instance.StartCoroutine(RequestGameModeDataCoroutine());
    }

    private IEnumerator RequestGameModeDataCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        _loadedGameModeData = _gameModeDataSource.GameModeData;
        OnGameModeLoaded?.Invoke();
    }
}
