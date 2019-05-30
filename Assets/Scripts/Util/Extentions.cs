using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Extension
{
    public static void SafeInvoke(this Action self)
    {
        self?.Invoke();
    }
}
