using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterEntity : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _aimMaxAngle = 45;

    [SerializeField]
    private float _aimMaxRange = 5.0f;

    [SerializeField]
    private float _shotSpeed = 10;

    [SerializeField]
    private float _recoilForce = 3;

    [SerializeField]
    private float _recoilDeacceleration = 3;

    [SerializeField]
    private float _walkingSpeed = 3;

    [SerializeField]
    private float _walkingAcceleration = 10.0f;

    [SerializeField]
    private Transform _turrentPivot;

    [SerializeField]
    private Transform _shotOrigin;

    [SerializeField]
    private SpriteRenderer _aimArrow;

    [SerializeField]
    private GameObject _crosshair;

    public event Action OnShotFinished;

    public GameObject BulletPrefab
    {
        get { return _bulletPrefab; }
    }
    public float ShotSpeed
    {
        get { return _shotSpeed; }
    }

    public float RecoilForce
    {
        get { return _recoilForce; }
    }

    public float WalkingSpeed
    {
        get { return _walkingSpeed; }
    }

    public float WalkingAcceleraation
    {
        get { return _walkingAcceleration; }
    }

    public float RecoilDeacceleration
    {
        get { return _recoilDeacceleration; }
    }

    public Transform TurrentPivot
    {
        get { return _turrentPivot; }
    }

    public Transform ShotOrigin
    {
        get { return _shotOrigin; }
    }

    public float AimMaxAngle
    {
        get { return _aimMaxAngle; }
    }

    public float AimMaxRange
    {
        get { return _aimMaxRange; }
    }

    public SpriteRenderer AimArrow
    {
        get { return _aimArrow; }
    }

    public GameObject CrossHair
    {
        get { return _crosshair; }
    }

    public Rigidbody2D Rigidbody { get; private set; }

    public void NotifyShot()
    {
        if(OnShotFinished != null)
        {
            OnShotFinished();
        }
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
