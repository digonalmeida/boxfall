using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarNightOverlay : GameAgent
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _minAlpha = 0.1f;
    
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _maxAlpha = 0.4f;
    
    private SpriteRenderer _spriteRenderer;
    private PowerUpData _powerUpData = null;
    private float _currentAlpha = 0;
    private float _targetAlpha = 0;
    private float _alphaPerSecond = 2.0f;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Reset()
    {
        _targetAlpha = 0;
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        GameEvents.OnActivatePowerUp += OnActivatePowerUp;
        GameEvents.OnDeactivatePowerUp += OnDeactivatePowerUp;
        Reset();
    }

    protected override void OnGameEnded()
    {
        base.OnGameEnded();
        GameEvents.OnDeactivatePowerUp -= OnActivatePowerUp;
        GameEvents.OnDeactivatePowerUp -= OnDeactivatePowerUp;
        Reset();
    }

    private void OnActivatePowerUp(PowerUpData powerUp)
    {
        if (powerUp.Type != EPowerUpType.Shield)
        {
            return;
        }
        
        _powerUpData = powerUp;
        UpdatePoweUpAlpha();
    }

    private void OnDeactivatePowerUp(PowerUpData powerUp)
    {
        if (powerUp.Type != EPowerUpType.Shield)
        {
            return;
        }
        
        _powerUpData = null;
        UpdatePoweUpAlpha();
    }

    private void UpdatePoweUpAlpha()
    {
        if (_powerUpData == null)
        {
            _targetAlpha = 0;
            return;
        }

        float ratio = _powerUpData.Time / _powerUpData.TotalTime;
        _targetAlpha = Mathf.Lerp(_minAlpha, _maxAlpha, ratio);
    }
    
    private void Update()
    {
        if (IsPaused)
        {
            return;
        }
        
        UpdatePoweUpAlpha();
        
        _currentAlpha = Mathf.MoveTowards(_currentAlpha, _targetAlpha, _alphaPerSecond * Time.deltaTime);
        var color = _spriteRenderer.color;
        color.a = _currentAlpha;
        _spriteRenderer.color = color;
    }
}
