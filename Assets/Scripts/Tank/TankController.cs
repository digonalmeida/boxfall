using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class TankController : GameAgent
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
    public TankEquipmentController TankEquipmentController { get; private set; }

    public AliveState AliveState { get; private set; }
    public DeadState DeadState { get; private set; }
    public IdleState IdleState { get; private set; }
    
    public GameObject SpriteObject
    {
        get { return _spriteObject; }
    }

    public GameObject ExplosionEffect
    {
        get { return _explosionEffect; }
    }

    protected override void Awake()
    {
        base.Awake();
        
        MovementController = InitializeComponent<TankMovementController>();
        TurrentController = InitializeComponent<TankTurrentController>();
        CollisionController = InitializeComponent<TankCollisionController>();
        TankPowerUpController = InitializeComponent<TankPowerUpController>();
        TankEquipmentController = InitializeComponent<TankEquipmentController>();
        
        AliveState = new AliveState();
        DeadState = new DeadState();
        IdleState = new IdleState();
        
        _stateMachine.Initialize(this);
        GameEvents.OnEnterTitleState += OnHomeScreen;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnterTitleState -= OnHomeScreen;
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
        _stateMachine.SetState(IdleState);
    }

    private void OnHomeScreen()
    {
        _stateMachine.SetState(IdleState);
    }
    
    protected override void OnGameStarted()
    {
        _stateMachine.SetState(AliveState);
    }

    private void Update()
    {
        if (IsPaused)
        {
            return;
        }
        
        _stateMachine.Update();
    }
}
