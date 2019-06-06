using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : GameAgent
{
    public GameObject _prefab;
    public float startDelay = 1.0f;
    public List<SpawnPoint> spawnPoints;
    private Coroutine _coroutine;

    [SerializeField]
    private AnimationCurve _frequencyOverTime = AnimationCurve.Constant(0, 1, 0.5f);
    
    [SerializeField]
    private float _maxFrequency = 2.0f;
        
    [SerializeField]
    private float _minFrequency = 0.1f;

    [SerializeField] 
    private int _minLevel = 0;
    
    [SerializeField] 
    private int _maxLevel = 5;


    private int _currentLevel;

    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }


    protected override void OnGameStarted()
    {
        GameController.Instance.LevelController.OnLevelChanged += OnLevelChanged;
        OnLevelChanged();
        StartSpawning();
    }

    protected override void OnGameEnded()
    {
        GameController.Instance.LevelController.OnLevelChanged -= OnLevelChanged;
        StopSpawning();
    }

    private void OnLevelChanged()
    {
        _currentLevel = GameController.Instance.LevelController.CurrentLevel;
    }
    
    private void StartSpawning()
    {
        _currentLevel = 1;
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
                continue;
            }

            if(sp.minLevel > _currentLevel)
            {
                continue;
            }

            spawnPoints.Add(sp);
        }
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(startDelay);
        float timeout = startDelay;
        
        for (; ; )
        {
            if (IsPaused)
            {
                yield return null;
                continue;
            }
            
            if (_currentLevel < _minLevel)
            {
                yield return null;
                continue;
            }

            timeout -= Time.deltaTime;
            
            if (timeout > 0)
            {
                yield return null;
                continue;
            }
            
            Spawn();
            
            timeout = GetSpawnInterval();
            yield return null;
        }
    }

    private float GetSpawnInterval()
    {
        float clampedLevel = Mathf.Clamp(_currentLevel, _minLevel, _maxLevel);
        float normalizedLevel = (clampedLevel - _minLevel) / (_maxLevel - _minLevel);
        float normalizedFrequency = _frequencyOverTime.Evaluate(normalizedLevel);
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
