using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TankData
{
    [Header("Moving Forward")] 
    [SerializeField] private float _walkingSpeed = 8.0f;
    [SerializeField] private float _walkingStoppingDistance = 0.5f;
    [SerializeField] private float _centerX = 1.3f;
    
    [Header("Recoil")] 
    [SerializeField] private float _recoilForce = 4.0f;
    [SerializeField] private float _maxRecoilForce = 7.0f;
    [SerializeField] private float _recoilDeacceleration = 10.0f;
    [SerializeField] private float _minX = -2.17f;

    public float CenterX => _centerX;
    public float RecoilForce => _recoilForce;
    public float MaxRecoilForce => _maxRecoilForce;
    public float WalkingSpeed => _walkingSpeed;
    public float WalkingStoppingDistance => _walkingStoppingDistance;
    public float RecoilDeacceleration => _recoilDeacceleration;
    public float MinX => _minX;
}
