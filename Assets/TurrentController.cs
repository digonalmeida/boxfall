using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TurrentController : MonoBehaviour
{
    private Animator _animator;
    private const string _shotAnimationTrigger = "shot";
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayShotAnimation()
    {
        _animator.SetTrigger(_shotAnimationTrigger);
    }
}
