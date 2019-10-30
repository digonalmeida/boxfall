using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModeDataLoader
{
    event Action OnGameModeLoaded;
    void RequestGameModeData();
    GameModeData LoadedGameModeData { get; }
}
