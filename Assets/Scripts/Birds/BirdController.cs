using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : GameAgent
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private GameObject _explosionEffect = null;

    private Rigidbody2D _rigidbody;
    private Collider2D[] _colliders;
    
    public bool Alive { get; private set; }
    public event Action OnKilled;

    private Vector2 _pausedVelocity;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody2D>();
        _colliders = GetComponents<Collider2D>();
        Alive = true;
    }

    protected override void OnGameEnded()
    {
        base.OnGameEnded();

        DestroyBird();
    }

    protected override void OnGamePaused()
    {
        base.OnGamePaused();
        _pausedVelocity = _rigidbody.velocity;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector2.zero;
    }
    
    protected override void OnGameUnpaused()
    {
        base.OnGameUnpaused();
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = _pausedVelocity;
    }

    public void DestroyBird()
    {
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector3.zero;
        _explosionEffect.SetActive(true);
        _spriteRenderer.enabled = false;
        
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
        Alive = false;
        Destroy(gameObject, 1.0f);
    }

    public void KillBird()
    {
        if(!Alive)
        {
            return;
        }
        GameEvents.NotifyBirdKilled();
        OnKilled?.Invoke();
        DestroyBird();
    }
}
