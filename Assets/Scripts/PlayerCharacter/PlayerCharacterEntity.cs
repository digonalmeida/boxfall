using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

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
    private float _stoppingDistance = 1.0f;

    [SerializeField]
    private Transform _turrentPivot;

    [SerializeField]
    private Transform _shotOrigin;

    [SerializeField]
    private SpriteRenderer _aimArrow;

    [SerializeField]
    private GameObject _crosshair;

    [SerializeField]
    private GameObject _explosionEffect;

    [SerializeField]
    private GameObject _spriteObject;

   
    [SerializeField]
    private float _centerX;

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

    public float WalkingStoppingDistance
    {
        get { return _stoppingDistance; }
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

    public GameObject ExplosionEffect
    {
        get { return _explosionEffect; }
    }

    public GameObject SpriteObject
    {
        get { return _spriteObject; }
    }

    public float CenterX
    {
        get { return _centerX; }
    }

    public StateMachine<PlayerCharacterEntity> StateMachine { get; set; }
    public static readonly AliveState AliveState;
    public static readonly DeadState DeadState;

    public PlayerMovementController MovementController { get; private set; }
    public PlayerShootingController TurrentController { get; private set; }
    public PlayerCharacterStateController StateController { get; private set; }
    public PlayerCharacterCollision CollisionController { get; private set; }
    public PlayerCharacterEvents Events { get; private set; }
    public Collider2D Collider { get; private set; }
    
    public Rigidbody2D Rigidbody { get; private set; }

    static PlayerCharacterEntity()
    {
        AliveState = new AliveState();
        DeadState = new DeadState();
    }

   
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        MovementController = GetComponent<PlayerMovementController>();
        TurrentController = GetComponent<PlayerShootingController>();
        StateController = GetComponent<PlayerCharacterStateController>();
        CollisionController = GetComponent<PlayerCharacterCollision>();
        Collider = GetComponent<Collider2D>();
        
        Events = GetComponent<PlayerCharacterEvents>();
        Events.Entity = this;
    }
}
