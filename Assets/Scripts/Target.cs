using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public Rigidbody2D Rigidbody { get; private set; }
    
    public Collider2D Collider { get; private set; }

    public GameObject ExplosionEffect;
    public bool Alive { get; private set; }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Alive = true;
        Destroy(gameObject, 3.0f);
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
        Collider.enabled = false;
        Alive = false;
        Destroy(gameObject, 1);
    }
}
