using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnerV2
{
    public class Spawner : GameAgent
    {
        [SerializeField] 
        private SpawnerDataSource _dataSource;
        
        [SerializeField] 
        private bool _testing;
        
        private float _testingCooldown = 0;
        private const float _testingIntervalSeconds = 1.0f;
        [SerializeField] private int _currentLevel;

        [SerializeField]
        private List<SpawningInstance> _spawningInstancesBag = new List<SpawningInstance>(10);
        
        private Coroutine _coroutine;

        protected override void OnGameStarted()
        {
            GameController.Instance.ScoringSystem.OnLevelChanged += OnLevelChanged;
            OnLevelChanged();
            StartSpawning();
        }
        
        protected override void OnGameEnded()
        {
            GameController.Instance.ScoringSystem.OnLevelChanged -= OnLevelChanged;
            StopSpawning();
        }
        
        private void OnLevelChanged()
        {
            _currentLevel = GameController.Instance.ScoringSystem.CurrentLevel;
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
        
        private void Update()
        {
            if (!_testing)
            {
                return;
            }

            _testingCooldown -= Time.deltaTime;
            if (_testingCooldown <= 0)
            {
                _testingCooldown = _testingIntervalSeconds;
                SpawnNext();
            }
        }

        private void SpawnNext()
        {
            if (_spawningInstancesBag.Count == 0)
            {
                RefreshPoints();
            }
            int spawnInstanceIndex = Random.Range(0, _spawningInstancesBag.Count);
            var spawningInstance = _spawningInstancesBag[spawnInstanceIndex];
            
            Spawn(spawningInstance);
            _spawningInstancesBag.RemoveAt(spawnInstanceIndex);
        }

        private void RefreshPoints()
        {
            _spawningInstancesBag.Clear();
            
            List<SpawningInstance> addingInstances = _dataSource.SpawnerData.SpawningInstances();
            foreach (SpawningInstance instance in addingInstances)
            {
                if (instance.MinLevel > _currentLevel)
                {
                    continue;
                }
                
                _spawningInstancesBag.Add(instance);
            }
        }

        private void Spawn(SpawningInstance spawningInstance)
        { 
            GameObject prefab = spawningInstance.Prefab;
            GameObject instance = PoolManager.Instance.GetInstance(prefab);
            
            instance.transform.position = spawningInstance.Position;

            ThrowObject(instance, spawningInstance.Angle, spawningInstance.Force);
        }

        private void ThrowObject(GameObject instance, float angle, float force)
        {
            Rigidbody2D instanceRigidbody = instance.GetComponent<Rigidbody2D>();
            Quaternion rotationAngle = Quaternion.Euler(0, 0, -angle);

            instanceRigidbody.velocity = rotationAngle * Vector2.left * force;
        }
        
        private float GetSpawnInterval()
        {
            int minLevel = _dataSource.SpawnerData.MinLevel;
            int maxLevel = _dataSource.SpawnerData.MaxLevel;
            AnimationCurve frequencyOverTime = _dataSource.SpawnerData.FrequencyOverTime;
            float minFrequency = _dataSource.SpawnerData.MinFrequency;
            float maxFrequency = _dataSource.SpawnerData.MaxFrequency;
            int clampedLevel = Mathf.Clamp(_currentLevel, minLevel, maxLevel);
            
            float normalizedLevel = ((float) (clampedLevel - minLevel)) / ((float)(maxLevel - minLevel));
            float normalizedFrequency = frequencyOverTime.Evaluate(normalizedLevel);
            float spawnFrequency = Mathf.Lerp(minFrequency, maxFrequency, normalizedFrequency);
            return 1.0f / spawnFrequency;
        }
        
        private IEnumerator SpawnRoutine()
        {
            float startDelay = _dataSource.SpawnerData.StartDelay;
            yield return new WaitForSeconds(startDelay);
            float timeout = startDelay;
        
            for (; ; )
            {
                if (IsPaused)
                {
                    yield return null;
                    continue;
                }
            
                if (_currentLevel < _dataSource.SpawnerData.MinLevel)
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
            
                SpawnNext();
            
                timeout = GetSpawnInterval();
                yield return null;
            }
        }
    }
}
