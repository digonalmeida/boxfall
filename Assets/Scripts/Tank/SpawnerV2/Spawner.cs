using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnerV2
{
    public class Spawner : MonoBehaviour
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
        
        
        private void Update()
        {
            if (_testing)
            {
                _testingCooldown -= Time.deltaTime;
                if (_testingCooldown <= 0)
                {
                    _testingCooldown = _testingIntervalSeconds;
                    SpawnNext();
                }
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
            List<SpawningInstance> addingInstances = _dataSource.SpawnerData.SpawningInstances();

            foreach (SpawningInstance instance in addingInstances)
            {
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
    }
}
