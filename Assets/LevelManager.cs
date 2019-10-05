using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameModeDataSource _gameModeDataSource;

    [SerializeField] private PoolManager _poolManager;

    private GameModeData _gameModeData;
    public void Awake()
    {
        _gameModeData = _gameModeDataSource.GameModeData;
        InitializeGameModeData();
    }

    public void InitializeGameModeData()
    {
        _poolManager.Initialize(_gameModeData.TurrentData, _gameModeData.Spawners);
    }
}
