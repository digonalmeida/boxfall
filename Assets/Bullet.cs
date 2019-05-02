using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<Target>();
        if(target != null)
        {
            target.DestroyTarget();
            Destroy(gameObject);

        }
    }
}
