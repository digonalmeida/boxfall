using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnTankDestroyed;
    public static event Action OnBirdKilled;
    public static event Action OnRequestUpdateUI;

    public static void NotifyTankDestroyed()
    {
        OnTankDestroyed?.Invoke();
    }

    public static void NotifyBirdKilled()
    {
        OnBirdKilled?.Invoke();
    }

    public static void RequestUpdateUI()
    {
        OnRequestUpdateUI?.Invoke();
    }
}
