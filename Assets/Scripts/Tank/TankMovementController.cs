using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementController : TankComponent
{
    [Header("Moving Forward")] [SerializeField]
    private float _walkingSpeed = 1.0f;

    [SerializeField] private float _walkingStoppingDistance = 0.5f;

    [SerializeField] private float _centerX = 0.0f;

    [Header("Recoil")] [SerializeField] private float _recoilForce = 1.0f;

    [SerializeField] private float _maxRecoilForce = 1.0f;

    [SerializeField] private float _recoilDeacceleration = 1.0f;

    private Rigidbody2D _rigidbody;

    private Vector2 _pausedVelocity;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
    }
    
    private void Start()
    {
        Tank.TurrentController.OnShot += OnShot;
    }

    private void OnDestroy()
    {
        Tank.TurrentController.OnShot -= OnShot;
    }

    public void Reset()
    {
        var pos = transform.position;
        pos.x = _centerX;
        transform.position = pos;
        _rigidbody.velocity = Vector2.zero;
    }

    protected override void OnGamePaused()
    {
        base.OnGamePaused();
        _pausedVelocity = _rigidbody.velocity;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.isKinematic = true;
    }

    protected override void OnGameUnpaused()
    {
        base.OnGameUnpaused();
        _rigidbody.velocity = _pausedVelocity;
        _rigidbody.isKinematic = false;
    }

    private void Update()
    {
        if (IsPaused)
        {
            return;
        }
        
        if(!enabled)
        {
            return;
        }

        if(!IsMovingForward())
        {
            UpdateRecoilDeacceleration();
        }
        else if(IsInsideStoppingDistance())
        {
            UpdateWalkingDeacceleration();
        }
        else
        {
            UpdateMovingForward();
        }
    }

    private void OnShot()
    {
        var vel = _rigidbody.velocity;

        if(vel.x > 0 || true)
        {
            vel = Vector2.zero;
        }

        vel += Vector2.left * _recoilForce;
        vel = Vector2.ClampMagnitude(vel, _maxRecoilForce);

        _rigidbody.velocity = vel;
    }

    private float GetDistanceFromCenter()
    {
        return _centerX - transform.position.x;
    }
    
    private void UpdateWalkingDeacceleration()
    {
        var distanceFromCenter = GetDistanceFromCenter();
        float distanceRatio = distanceFromCenter / _walkingStoppingDistance;

        var vel = Vector2.Lerp(Vector2.right * _walkingSpeed, Vector2.zero, 1 - distanceRatio);
        
        if (vel.magnitude > _rigidbody.velocity.magnitude)
        {
            return;
        }

        _rigidbody.velocity = vel;
    }

    private void UpdateMovingForward()
    {
        _rigidbody.AddForce(Vector2.right * _walkingSpeed);
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _walkingSpeed);
    }

    private void UpdateRecoilDeacceleration()
    {
        _rigidbody.AddForce(Vector2.right * _recoilDeacceleration);
    }

    private bool IsMovingForward()
    {
        return _rigidbody.velocity.x >= 0;
    }

    private bool IsInsideStoppingDistance()
    {
        var distanceFromCenter = GetDistanceFromCenter();
        return distanceFromCenter < _walkingStoppingDistance;
    }
}
