using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementController : TankComponent
{
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
        pos.x = TankData.CenterX;
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

        vel += Vector2.left * TankData.RecoilForce;
        vel = Vector2.ClampMagnitude(vel, TankData.MaxRecoilForce);

        _rigidbody.velocity = vel;
    }

    private float GetDistanceFromCenter()
    {
        return TankData.CenterX - transform.position.x;
    }
    
    private void UpdateWalkingDeacceleration()
    {
        var distanceFromCenter = GetDistanceFromCenter();
        float distanceRatio = distanceFromCenter / TankData.WalkingStoppingDistance;

        var vel = Vector2.Lerp(Vector2.right * TankData.WalkingSpeed, Vector2.zero, 1 - distanceRatio);
        
        if (vel.magnitude > _rigidbody.velocity.magnitude)
        {
            return;
        }

        _rigidbody.velocity = vel;
    }

    private void UpdateMovingForward()
    {
        _rigidbody.velocity += Vector2.right * TankData.WalkingSpeed * Time.deltaTime;
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, TankData.WalkingSpeed);
    }

    private void UpdateRecoilDeacceleration()
    {
        
        if (transform.position.x <= TankData.MinX)
        {
            var pos = transform.position;
            pos.x = TankData.MinX;
            transform.position = pos;
            _rigidbody.velocity = Vector2.zero;
        }
        
        _rigidbody.velocity += Vector2.right * TankData.RecoilDeacceleration * Time.deltaTime;
    }

    private bool IsMovingForward()
    {
        return _rigidbody.velocity.x >= 0;
    }

    private bool IsInsideStoppingDistance()
    {
        var distanceFromCenter = GetDistanceFromCenter();
        return distanceFromCenter < TankData.WalkingStoppingDistance;
    }
}
