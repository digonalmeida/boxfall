using System;
using System.Collections;
using System.Collections.Generic;
using Birds;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PoolableObject))]
public class BirdController : GameAgent
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private GameObject _explosionEffect = null;

    private Rigidbody2D _rigidbody;
    private Collider2D[] _colliders;
    private BirdComponent[] _components;
    private PoolableObject _poolable;
    private BirdData _birdData;
    
    public bool Alive { get; private set; }
    public BirdColor BirdColor => _birdData?.Color ?? BirdColor.None;
    public event Action OnKilled;

    private Vector2 _pausedVelocity;

   
    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody2D>();
        _colliders = GetComponents<Collider2D>();
        _poolable = GetComponent<PoolableObject>();
        _components = GetComponents<BirdComponent>();
        _poolable.OnShow += OnShow;
    }

    public void SetData(BirdData birdData)
    {
        _birdData = birdData;
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

    private void OnShow()
    {
        gameObject.SetActive(true);
        Alive = true;
        _explosionEffect.SetActive(false);
        _spriteRenderer.enabled = true;
        _spriteRenderer.gameObject.SetActive(true);
        
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
        foreach(var component in _components)
        {
            component.OnShow();
        }
        
        GameEvents.NotifyBirdSpawned();
    }

    public void DestroyBird()
    {
        if(!Alive)
        {
            return;
        }
        _rigidbody.velocity = Vector3.zero;
        _explosionEffect.SetActive(true);
        _spriteRenderer.enabled = false;
        _spriteRenderer.gameObject.SetActive(false);
        
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }

        Alive = false;
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        foreach(var component in _components)
        {
            component.OnHide();
        }

        gameObject.SetActive(false);
        _poolable.Recycle();
    }

    public void KillBird()
    {
        if(!Alive)
        {
            return;
        }
        GameEvents.NotifyBirdKilled(this);
        OnKilled?.Invoke();
        DestroyBird();
    }

    public void SetupSpriteInstance(SpriteRenderer spriteInstance)
    {
        spriteInstance.transform.parent = transform;
        _spriteRenderer = spriteInstance;
        spriteInstance.transform.position = transform.position;
    }
}
