using System;
using System.Collections;
using System.Collections.Generic;
using Birds;
using SpawnerV2;
using UnityEngine;

[Serializable]
public class GameModeData
{
    [SerializeField] private TankData _tankData;
    [SerializeField] private TurrentData _turrentData;
    [SerializeField] private SpawnerData[] _spawners;
    [SerializeField] private BirdData[] _birds;
    public TankData TankData => _tankData;
    public SpawnerData[] Spawners => _spawners;
    public TurrentData TurrentData => _turrentData;
    public BirdData[] Birds => _birds;
}
