using System.Collections;
using System.Collections.Generic;
using SpawnerV2;
using UnityEngine;

[CreateAssetMenu]
public class SpawnerDataSource : ScriptableObject
{
    [SerializeField]
    private SpawnerData _spawnerData;

    public SpawnerData SpawnerData => _spawnerData;
}
