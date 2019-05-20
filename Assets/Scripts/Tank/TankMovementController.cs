using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementController : TankComponent
{
    [Header("Moving Forward")]
    [SerializeField]
    private float _walkingSpeed;

    [SerializeField]
    private float _walkingStoppingDistance;

    [SerializeField]
    private float _centerX;

    [Header("Recoil")]
    [SerializeField]
    private float _recoilForce;

    [SerializeField]
    private float _maxRecoilForce;

    [SerializeField]
    private float _recoilDeacceleration;
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Enable()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void Disable()
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
    
    private void Update()
    {
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
