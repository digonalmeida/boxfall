using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : UiElement
{
    public void Pause()
    {
        GameEvents.NotifyGamePaused();
    }
}
