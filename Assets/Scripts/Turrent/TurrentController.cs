using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TurrentController : MonoBehaviour
{
    private const string _shotAnimationTrigger = "shot";
    private const string _invulnerableAnimationProperty = "Invulnerable";

    [SerializeField]
    private ObjectPool _bulletPool;

    [SerializeField]
    private float _shotSpeed = 17.0f;

    [SerializeField]
    private Transform _shotOrigin = null;
    
    private Animator _animator;
    
    public void Fire()
    {
        Bullet shot = _bulletPool.GetInstance().GetComponent<Bullet>();

        var position = _shotOrigin.transform.position;
        var direction = transform.right;
        

        shot.Fire(position, direction, _shotSpeed);

        PlayShotAnimation();
    }

    public void SetInvulnerable(bool invulnerable)
    {
        if(invulnerable)
        {
            PlayInvulnerableAnimation();
        }
        else
        {
            StopInvulnerableAnimation();
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void PlayShotAnimation()
    {
        _animator.SetTrigger(_shotAnimationTrigger);
    }

    private void PlayInvulnerableAnimation()
    {
        _animator.SetBool(_invulnerableAnimationProperty, true);
    }

    private void StopInvulnerableAnimation()
    {
        _animator.SetBool(_invulnerableAnimationProperty, false);
    }
}
