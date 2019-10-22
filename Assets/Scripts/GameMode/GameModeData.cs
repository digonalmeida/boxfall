using System;
using System.Collections;
using System.Collections.Generic;
using Birds;
using SpawnerV2;
using UnityEngine;

[Serializable]
public class GameModeData
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private TankData _tankData;
    [SerializeField] private TurrentData _turrentData;
    [SerializeField] private SpawnerData[] _spawners = new SpawnerData[0];
    [SerializeField] private BirdData[] _birds = new BirdData[0];
    [SerializeField] private SpawnPointData[] _spawnPoints = new SpawnPointData[0];

    public string Name => _name;

    public string Description => _description;
    public TankData TankData => _tankData;
    public SpawnerData[] Spawners => _spawners;
    public TurrentData TurrentData => _turrentData;
    public BirdData[] Birds => _birds;
    public SpawnPointData[] SpawnPoints => _spawnPoints;
}
