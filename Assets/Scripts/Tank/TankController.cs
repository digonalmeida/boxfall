using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class TankController : MonoBehaviour
{
    [SerializeField]
    private GameObject _spriteObject;

    [SerializeField]
    private GameObject _explosionEffect;

    public GameObject SpriteObject
    {
        get { return _spriteObject; }
    }

    public GameObject ExplosionEffect
    {
        get { return _explosionEffect; }
    }
    
    public StateMachine<TankController> StateMachine { get; set; }

    public static readonly AliveState AliveState;
    public static readonly DeadState DeadState;

    public TankMovementController MovementController { get; private set; }
    public TankTurrentController TurrentController { get; private set; }
    public TankCollisionController CollisionController { get; private set; }
    public Collider2D Collider { get; private set; }

    static TankController()
    {
        AliveState = new AliveState();
        DeadState = new DeadState();
    }
    
    private void Awake()
    {
        MovementController = InitializeComponent<TankMovementController>();
        TurrentController = InitializeComponent<TankTurrentController>();
        CollisionController = InitializeComponent<TankCollisionController>();
        StateMachine = new StateMachine<TankController>();
        StateMachine.Initialize(this);
    }

    private T InitializeComponent<T>() where T: TankComponent
    {
        var component = GetComponent<T>();
        component?.Initialize(this);
        return component;
    }

    private void Start()
    {
        gameObject.SetActive(false);
        GameEvents.OnGameStarted += OnGameStarted;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        gameObject.SetActive(true);
        SetState(AliveState);
    }

    private void Update()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        StateMachine.Update();
    }

    public void SetState(State<TankController> state)
    {
        StateMachine.SetState(state);
    }
}
