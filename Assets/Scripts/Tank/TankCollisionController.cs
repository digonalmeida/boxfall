using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TankCollisionController : TankComponent
{
    private Collider2D _collider = null;

    public delegate void TargetHitDelegate(BirdController birdController);
    public event TargetHitDelegate OnHitByTarget;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (!enabled)
        {
            return;
        }

        var target = other.collider.GetComponent<BirdController>();
        if (target != null)
        {
            OnHitByTarget?.Invoke(target);
        }
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }
}
