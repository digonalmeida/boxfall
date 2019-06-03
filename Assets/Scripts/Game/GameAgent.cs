using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAgent : MonoBehaviour
{
    protected bool IsPaused { get; private set; }
    
    protected virtual void Awake()
    {
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameEnded += OnGameEnded;
        GameEvents.OnGamePaused += OnGamePausedInternal;
        GameEvents.OnGameUnpaused += OnGameUnpausedInternal;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= OnGameStarted;
        GameEvents.OnGameEnded -= OnGameEnded;
        GameEvents.OnGamePaused -= OnGamePausedInternal;
        GameEvents.OnGameUnpaused -= OnGameUnpausedInternal;
    }

    protected virtual void OnGameStarted()
    {
        IsPaused = false;
    }

    protected virtual void OnGameEnded()
    {
        IsPaused = false;
    }

    private void OnGamePausedInternal()
    {
        if (IsPaused)
        {
            return;
        }
        
        OnGamePaused();
    }
    
    private void OnGameUnpausedInternal()
    {
        if (!IsPaused)
        {
            return;
        }
        
        OnGameUnpaused();
    }

    protected virtual void OnGamePaused()
    {
        IsPaused = true;
    }

    protected virtual void OnGameUnpaused()
    {
        IsPaused = false;
    }
}
