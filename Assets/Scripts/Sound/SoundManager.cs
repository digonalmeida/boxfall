using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GameAgent
{
    [SerializeField]
    private AudioSource _shot = null;

    [SerializeField]
    private AudioSource _enemySpawn = null;

    [SerializeField]
    private AudioSource _enemyDie = null;

    [SerializeField]
    private AudioSource _uiAccept = null;

    [SerializeField] 
    private AudioSource _titleBgm = null;

    [SerializeField] 
    private AudioSource _gameplayBgm = null;

    [SerializeField]
    private AudioSource _starPowerBgm = null;

    private AudioSource[] _bgms;
    private PowerUpsManager _powerUpsManager;

    protected override void Awake()
    {
        base.Awake();

        _bgms = new[] {_titleBgm, _gameplayBgm, _starPowerBgm};
        
        _powerUpsManager = GameController.Instance.PowerUpsManager;
        GameEvents.OnShotFired += OnShotFired;
        GameEvents.OnBirdKilled += OnBirdKilled;
        GameEvents.OnBirdSpawned += OnBirdSpawned;
        GameEvents.OnUiAccept += OnUiAccept;
        _powerUpsManager.OnActivatePowerUp += OnActivatePowerUp;
        _powerUpsManager.OnDeactivatePowerUp += OnDeactivatePowerUp;
    }

    private void Start()
    {
        PlayBgm(_titleBgm);
    }
    
    
    private void OnDestroy()
    {
        GameEvents.OnShotFired -= OnShotFired;
        GameEvents.OnBirdKilled -= OnBirdKilled;
        GameEvents.OnBirdSpawned -= OnBirdSpawned;
        GameEvents.OnUiAccept -= OnUiAccept;
        if (_powerUpsManager != null)
        {
            _powerUpsManager.OnActivatePowerUp -= OnActivatePowerUp;
            _powerUpsManager.OnDeactivatePowerUp -= OnDeactivatePowerUp;
        }
    }

    private void OnShotFired()
    {
        PlaySfx(_shot);
    }

    private void OnBirdKilled(BirdController bird)
    {
        PlaySfx(_enemyDie);
    }

    private void OnBirdSpawned()
    {
        PlaySfx(_enemySpawn);
    }

    private void OnUiAccept()
    {
        PlaySfx(_uiAccept);
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();
        PlayBgm(_gameplayBgm);
    }

    protected override void OnGameEnded()
    {
        base.OnGameEnded();
        PlayBgm(_titleBgm);
    }

    private void OnActivatePowerUp(PowerUpData data)
    {
        PlayBgm(_titleBgm);
    }

    private void OnDeactivatePowerUp(PowerUpData data)
    {
        PlayBgm(_gameplayBgm);
    }

    private void PlaySfx(AudioSource sfx)
    {
        sfx?.Play();
    }

    private void PlayBgm(AudioSource audioSource)
    {
        foreach (var bgm in _bgms)
        {
            if (bgm == audioSource)
            {
                if (!bgm.isPlaying)
                {
                    bgm.Play();
                }
            }
            else
            {
                bgm.Stop();
            }
        }
    }
}
