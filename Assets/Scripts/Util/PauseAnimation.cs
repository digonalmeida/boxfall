using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnimation : GameAgent
{
    private Animator _animator;
    private TrailRenderer _trailRenderer;
    private ParticleSystem _particleSystem;

    private float _pausedAnimatorSpeed = 1;
    private float _pausedTrailTime;
    
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    protected override void OnGamePaused()
    {
        base.OnGamePaused();
        PauseAnimator();
        PauseTrail();
        PauseParticleSystem();
    }

    protected override void OnGameUnpaused()
    {
        base.OnGameUnpaused();
        UnpauseAnimator();
        UnpauseTrail();
        UnpauseParticleSystem();
    }

    private void PauseAnimator()
    {
        if (_animator == null)
        {
            return;
        }
        
        _pausedAnimatorSpeed = _animator.speed;
        _animator.speed = 0;
    }

    private void UnpauseAnimator()
    {
        if (_animator == null)
        {
            return;
        }
        _animator.speed = _pausedAnimatorSpeed;
        
    }
    
    private void PauseTrail()
    {
        if (_trailRenderer == null)
        {
            return;
        }

        _pausedTrailTime = _trailRenderer.time;
        _trailRenderer.time = float.PositiveInfinity;
    }

    private void UnpauseTrail()
    {
        if (_trailRenderer == null)
        {
            return;
        }

        _trailRenderer.time = _pausedTrailTime;
    }

    private void PauseParticleSystem()
    {
        if (_particleSystem == null)
        {
            return;
        }
        
        _particleSystem.Pause(true);
    }

    private void UnpauseParticleSystem()
    {
        if (_particleSystem == null)
        {
            return;
        }
        
        _particleSystem.Play(true);
    }
}
