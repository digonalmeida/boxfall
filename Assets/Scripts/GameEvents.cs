﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public event Action OnTankDestroyed; 
    public void NotifyTankDestroyed()
    {
        
    }
}
