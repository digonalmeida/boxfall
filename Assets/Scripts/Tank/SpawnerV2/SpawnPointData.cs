using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnPointData
{
    [SerializeField] 
    private GameObject _prefab = null;

    [SerializeField] private float _angle;
    [SerializeField] private float _force;
    public GameObject Prefab => _prefab;
    public float Angle => _angle;
    public float Force => _force;
}
