using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using SpawnerV2;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<Spawner> _spawners = new List<Spawner>();

    public void Start()
    {
        SpawnerData[] _spawnerDatas = GameModesManager.Instance.GameModeData.Spawners;
        foreach (SpawnerData spawnerData in _spawnerDatas)
        {
            InstantiateSpawner(spawnerData);
        }
    }
    
    public Spawner InstantiateSpawner(SpawnerData spawnerData)
    {
        var spawnerGameObject = new GameObject(spawnerData.Name + "_spawner", typeof(Spawner));
        spawnerGameObject.transform.parent = transform;
        Spawner spawner = spawnerGameObject.GetComponent<Spawner>();
        _spawners.Add(spawner);
        spawner.Initialize(spawnerData);
        return spawner;
    }

    public void ClearSpawners()
    {
        foreach (var spawner in _spawners)
        {
            Destroy(spawner.gameObject);
        }
        _spawners.Clear();
    }
}