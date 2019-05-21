using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForward : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void Update()
    {
        if (_rigidbody.velocity.magnitude <= 0.001f)
        {
            return;
        }
        transform.right = -_rigidbody.velocity.normalized;
    }
}
