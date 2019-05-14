using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject _prefab;
    public float startDelay = 1.0f;
    public List<SpawnPoint> spawnPoints;
    private Coroutine _coroutine;

    [SerializeField]
    private AnimationCurve _frequencyOverTime;
    
    [SerializeField]
    private float _maxFrequency = 2.0f;
        
    [SerializeField]
    private float _minFrequency = 0.1f;

    [SerializeField] 
    private float _maxGameplayTime = 3 * 60;

    private float _currentGameplayTime;

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
        _currentGameplayTime = 0;
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
        spawnPoints = new List<SpawnPoint>();
        foreach(Transform child in transform)
        {
            if(!child.gameObject.activeInHierarchy)
            {
                continue;
            }

            var sp = child.GetComponent<SpawnPoint>();
            if (sp == null)
            {
                return;
            }
            spawnPoints.Add(sp);
        }
    }

    private IEnumerator SpawnRoutine()
    {
        _currentGameplayTime = 0;
        yield return new WaitForSeconds(startDelay);
        for (; ; )
        {
            Spawn();
            float spawnInterval = GetSpawnInterval();
            yield return new WaitForSeconds(spawnInterval);
            _currentGameplayTime += spawnInterval;
        }
    }

    private float GetSpawnInterval()
    {
        float clampedGameplayTime = Mathf.Clamp(_currentGameplayTime, 0, _maxGameplayTime);
        float normalizedGameplayTime = clampedGameplayTime / _maxGameplayTime;
        float normalizedFrequency = _frequencyOverTime.Evaluate(normalizedGameplayTime);
        float spawnFrequency = Mathf.Lerp(_minFrequency, _maxFrequency, normalizedFrequency);
        return 1.0f / spawnFrequency;
    }

    private void Spawn()
    {
        if(spawnPoints.Count == 0)
        {
            RefreshPoints();
        }

        int pointIndex = Random.Range(0, spawnPoints.Count);
        var sp = spawnPoints[pointIndex];
        
        var prefab = sp.OverridePrefab != null
            ? sp.OverridePrefab
            : _prefab;
        
        var crate = Instantiate(prefab);
      
        crate.transform.position = sp.transform.position;
        crate.transform.localScale = sp.transform.localScale;
        
        crate.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0,0,-sp.angle) * Vector2.left * sp.force;
        spawnPoints.RemoveAt(pointIndex);
        GameEvents.NotifyBirdSpawned();
    }
    
    
}
