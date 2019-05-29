using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class TankController : MonoBehaviour
{
    [SerializeField]
    private GameObject _spriteObject = null;

    [SerializeField]
    private GameObject _explosionEffect = null;
 
    private StateMachine<TankController> _stateMachine = new StateMachine<TankController>();

    public TankMovementController MovementController { get; private set; }
    public TankTurrentController TurrentController { get; private set; }
    public TankCollisionController CollisionController { get; private set; }
    public TankPowerUpController TankPowerUpController { get; private set; }
    public AliveState AliveState { get; private set; }
    public DeadState DeadState { get; private set; }
    
    public GameObject SpriteObject
    {
        get { return _spriteObject; }
    }

    public GameObject ExplosionEffect
    {
        get { return _explosionEffect; }
    }

    private void Awake()
    {
        GameEvents.OnGameStarted += OnGameStarted;
        
        MovementController = InitializeComponent<TankMovementController>();
        TurrentController = InitializeComponent<TankTurrentController>();
        CollisionController = InitializeComponent<TankCollisionController>();
        TankPowerUpController = InitializeComponent<TankPowerUpController>();
        
        AliveState = new AliveState();
        DeadState = new DeadState();
        
        _stateMachine.Initialize(this);
    }

    private T InitializeComponent<T>() where T: TankComponent
    {
        var component = GetComponent<T>();
        if (component != null)
        {
            component.Initialize(this);
        }
        return component;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        gameObject.SetActive(true);
        _stateMachine.SetState(AliveState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}
