using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _shot = null;

    [SerializeField]
    private AudioSource _enemySpawn = null;

    [SerializeField]
    private AudioSource _enemyDie = null;

    [SerializeField]
    private AudioSource _uiAccept = null;

    private void Awake()
    {
        GameEvents.OnShotFired += OnShotFired;
        GameEvents.OnBirdKilled += OnBirdKilled;
        GameEvents.OnBirdSpawned += OnBirdSpawned;
        GameEvents.OnUiAccept += OnUiAccept;
    }

    private void OnDestroy()
    {
        GameEvents.OnShotFired -= OnShotFired;
        GameEvents.OnBirdKilled -= OnBirdKilled;
        GameEvents.OnBirdSpawned -= OnBirdSpawned;
        GameEvents.OnUiAccept -= OnUiAccept;
    }

    private void OnShotFired()
    {
        PlaySfx(_shot);
    }

    private void OnBirdKilled()
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
    
    private void PlaySfx(AudioSource sfx)
    {
        sfx?.Play();
    }
}
