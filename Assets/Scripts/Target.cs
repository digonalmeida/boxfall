using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public Rigidbody2D Rigidbody { get; private set; }
    
    public Collider2D[] Colliders { get; private set; }

    public GameObject ExplosionEffect;
    public bool Alive { get; private set; }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Colliders = GetComponents<Collider2D>();
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

    public void EnableTarget()
    {

    }

    public void DestroyTarget()
    {
        Rigidbody.gravityScale = 0;
        Rigidbody.velocity = Vector3.zero;
        ExplosionEffect.SetActive(true);
        _spriteRenderer.enabled = false;
        foreach (var collider in Colliders)
        {
            collider.enabled = false;
        }
        Alive = false;
        Destroy(gameObject, 1);
    }
}
