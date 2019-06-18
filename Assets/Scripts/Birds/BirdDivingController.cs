using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdDivingController : BirdComponent
{
    [SerializeField] 
    private float _aimAngle = 60;

    [SerializeField] 
    private float _diveSpeed = 5.0f;

    [SerializeField] 
    private LayerMask _layerMask;
    
    private bool _diving = false;

    private Rigidbody2D _rigidbody;
    
    private Vector2 AimDirection
    {
        get
        {
            return Quaternion.Euler(0, 0, _aimAngle) * Vector2.left;
        }
    }

    protected void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void OnShow()
    {
        base.OnShow();
        _diving = false;
    }

    public override void OnHide()
    {
        base.OnHide();
        _diving = false;
    }

    private void Update()
    {
        if(!enabled)
        {
            return;
        }
        
        if(!Bird.Alive)
        {
            return;
        }
        Debug.DrawRay(transform.position, AimDirection, Color.yellow, 1.0f);
        if (_diving)
        {
            return;
        }

        if (Scan())
        {
            StartDiving();
            _diving = true;
        }
    }

    private void StartDiving()
    {
        _rigidbody.velocity = AimDirection * _diveSpeed;
        //_rigidbody.gravityScale = 1;
    }

    private bool Scan()
    {
        var hit = Physics2D.Raycast(transform.position, AimDirection, 100,  _layerMask.value);
        return hit.collider != null;
    }

    
}
