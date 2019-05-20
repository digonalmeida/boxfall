using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TurrentController : MonoBehaviour
{
    private const string _shotAnimationTrigger = "shot";

    [SerializeField]
    private Bullet _bulletPrefab = null;

    [SerializeField]
    private float _shotSpeed = 17.0f;

    [SerializeField]
    private Transform _shotOrigin = null;
    
    private Animator _animator;

    public void Fire()
    {
        Bullet shot = Instantiate(_bulletPrefab);

        var position = _shotOrigin.transform.position;
        var direction = transform.right;

        shot.Fire(position, direction, _shotSpeed);

        PlayShotAnimation();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void PlayShotAnimation()
    {
        _animator.SetTrigger(_shotAnimationTrigger);
    }
}
