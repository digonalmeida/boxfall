using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : GameAgent
{
    private Rigidbody2D _rigidbody;

    private Vector2 _pausedVelocity;

    private PoolableObject _poolable;
    private bool _alive;
    public void Fire(Vector2 startPosition, Vector2 direction, float speed)
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        _rigidbody.velocity = direction * speed;
        transform.right = direction;

        GameEvents.NotifyShotFired();
    }

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
        _poolable = GetComponent<PoolableObject>();
        _poolable.OnShow += OnShow;
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
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = _pausedVelocity;
    }

    public void Update()
    {
        if (!_alive)
        {
            return;
        }

        if (transform.position.x > 6)
        {
            DestroyBullet();
            return;
        }

        if (transform.position.y < -8)
        {
            DestroyBullet();
            return;
        }
    }

    private void OnShow()
    {
        _alive = true;
    }
    
    public void DestroyBullet()
    {
        _alive = false;
        gameObject.SetActive(false);
        _poolable.Recycle();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var bird = other.GetComponent<BirdController>();
        if(bird != null)
        {
            bird.KillBird();
            DestroyBullet();
        }
    }
    
}
