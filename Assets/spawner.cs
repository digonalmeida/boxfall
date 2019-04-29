using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject _prefab;
    public float force = 2;
    public float angle = 45;
    public List<Transform> spawnPoints;
    private void Start()
    {
        spawnPoints = new List<Transform>();
        foreach(Transform child in transform)
        {
            spawnPoints.Add(child);
        }


        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        for (; ; )
        {
            Spawn();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Spawn()
    {
        var crate = Instantiate(_prefab);
        crate.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        crate.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0,0,-angle) * Vector2.left * force;
    }
}
