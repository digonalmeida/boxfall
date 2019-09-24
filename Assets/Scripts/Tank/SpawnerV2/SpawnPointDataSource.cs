using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnPointDataSource : ScriptableObject
{
    [SerializeField] 
    private SpawnPointData _spawnPointData;
    public SpawnPointData SpawnPointData => _spawnPointData;
}
