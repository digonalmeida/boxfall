using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : GameAgent
{
    private Rigidbody2D _rigidbody;

    private Vector2 _pausedVelocity;

    public void Fire(Vector2 startPosition, Vector2 direction, float speed)
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        _rigidbody.velocity = direction * speed;

        GameEvents.NotifyShotFired();
    }

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bird = other.GetComponent<BirdController>();
        if(bird != null)
        {
            bird.KillBird();
            Destroy(gameObject);
        }
    }
    
}
