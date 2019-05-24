using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public void Fire(Vector2 startPosition, Vector2 direction, float speed)
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        _rigidbody.velocity = direction * speed;

        GameEvents.NotifyShotFired();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2);
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
