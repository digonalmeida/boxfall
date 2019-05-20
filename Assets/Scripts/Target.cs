﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private GameObject _explosionEffect = null;

    private Rigidbody2D _rigidbody;
    private Collider2D[] _colliders;
    public bool Alive { get; private set; }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _colliders = GetComponents<Collider2D>();
        Alive = true;
        Destroy(gameObject, 10.0f);
        GameEvents.OnGameEnded += OnGameEnded;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameEnded -= OnGameEnded;
    }

    private void OnGameEnded()
    {
        DestroyTarget();
    }
    
    public void DestroyTarget()
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
        Destroy(gameObject, 1);
    }
}
