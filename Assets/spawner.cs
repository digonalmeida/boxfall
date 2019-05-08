using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject _prefab;
    public float force = 2;
    public float angle = 45;
    public float spawnInterval = 1.0f;
    public float spawnIntervalReduction = 0.1f;
    public float startDelay = 1.0f;
    public List<Transform> spawnPoints;
    private Coroutine _coroutine;

    private void Awake()
    {
        enabled = false;
        GameEvents.OnGameStarted += StartSpawning;
        GameEvents.OnGameEnded += StopSpawning;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= StartSpawning;
        GameEvents.OnGameEnded -= StopSpawning;
    }
    
    private void StartSpawning()
    {
        StopSpawning();
        RefreshPoints();
        _coroutine = StartCoroutine(SpawnRoutine());
    }

    private void StopSpawning()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void RefreshPoints()
    { 
        spawnPoints = new List<Transform>();
        foreach(Transform child in transform)
        {
            if(!child.gameObject.activeInHierarchy)
            {
                continue;
            }
            spawnPoints.Add(child);
        }
    }

    public void Update()
    {
        if (!enabled)
        {
            return;
        }
        
        spawnInterval -= Time.deltaTime * spawnIntervalReduction;
        if(spawnInterval <= 0.5f)
        {
            spawnInterval = 0.5f;
        }
    }
    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(startDelay);
        for (; ; )
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        if(spawnPoints.Count == 0)
        {
            RefreshPoints();
        }
        
        var crate = Instantiate(_prefab);
        int pointIndex = Random.Range(0, spawnPoints.Count);

        crate.transform.position = spawnPoints[pointIndex].position;
        crate.transform.localScale = spawnPoints[pointIndex].localScale;
        spawnPoints.RemoveAt(pointIndex);
        crate.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0,0,-angle) * Vector2.left * force;
    }
}
