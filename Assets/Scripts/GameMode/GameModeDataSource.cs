using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameModeDataSource : ScriptableObject
{
    [SerializeField] private GameModeData _gameModeData;

    public GameModeData GameModeData => _gameModeData;
}
