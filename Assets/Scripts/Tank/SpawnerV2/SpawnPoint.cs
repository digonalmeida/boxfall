using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnerV2
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private SpawnPointDataSource _spawnPointDataSource;
        
        public void Spawn()
        {
            GameObject prefab = _spawnPointDataSource.SpawnPointData.Prefab;
            GameObject instance = PoolManager.Instance.GetInstance(prefab);
            
            var myTransform = transform;
            instance.transform.position = myTransform.position;
            instance.transform.localScale = myTransform.localScale;
            ThrowObject(instance);
        }

        private void ThrowObject(GameObject instance)
        {
            Rigidbody2D instanceRigidbody = instance.GetComponent<Rigidbody2D>();
            float angle = _spawnPointDataSource.SpawnPointData.Angle;
            float force = _spawnPointDataSource.SpawnPointData.Force;
            Quaternion rotationAngle = Quaternion.Euler(0, 0, -angle);

            instanceRigidbody.velocity = rotationAngle * Vector2.left * force;
        }
    }
}