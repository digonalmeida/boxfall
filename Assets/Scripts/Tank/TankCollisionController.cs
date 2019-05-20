using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TankCollisionController : TankComponent
{
    private Collider2D _collider = null;

    public delegate void TargetHitDelegate(Target target);
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
        
        var target = other.collider.GetComponent<Target>();
        if (target != null)
        {
            OnHitByTarget?.Invoke(target);
        }
    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
        _collider.enabled = enabled;
    }
}
