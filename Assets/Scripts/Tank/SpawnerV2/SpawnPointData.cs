using System;
using UnityEngine;


[Serializable]
public class SpawnPointData
{
    [SerializeField] private string _name;
    [SerializeField] private Vector3 _position;
    [SerializeField] private float _angle;
    [SerializeField] private float _force;

    public string Name => _name;
    public Vector3 Position => _position;
    public float Angle => _angle;
    public float Force => _force;
}
