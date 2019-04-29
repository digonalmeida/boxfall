using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
