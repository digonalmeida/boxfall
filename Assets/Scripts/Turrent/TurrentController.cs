using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TurrentController : MonoBehaviour
{
    private static int _shotAnimationTriggerId;
    private static int _invulnerableAnimationPropertyId;

    [SerializeField]
    private Transform _shotOrigin = null;

    [SerializeField] 
    private TurrentData _turrentData;
    
    private Animator _animator;

    static TurrentController()
    {
        _shotAnimationTriggerId = Animator.StringToHash("shot");
        _invulnerableAnimationPropertyId = Animator.StringToHash("Invulnerable");
    }
    
    public void Fire()
    {
        var bulletPrefab = _turrentData.BulletPrefab.gameObject;
        Bullet shot = PoolManager.Instance.GetInstance(bulletPrefab).GetComponent<Bullet>();

        var position = _shotOrigin.transform.position;
        var direction = transform.right;
        

        shot.Fire(position, direction, _turrentData.ShotSpeed);

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
        _turrentData = GameModesManager.Instance.GameModeData.TurrentData;
    }
    
    private void PlayShotAnimation()
    {
        _animator.SetTrigger(_shotAnimationTriggerId);
    }

    private void PlayInvulnerableAnimation()
    {
        _animator.SetBool(_invulnerableAnimationPropertyId, true);
    }

    private void StopInvulnerableAnimation()
    {
        _animator.SetBool(_invulnerableAnimationPropertyId, false);
    }
}
