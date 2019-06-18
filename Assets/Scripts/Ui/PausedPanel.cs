using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedPanel : UiElement
{
    public void Unpause()
    {
        GameEvents.NotifyGameUnpaused();
    }

    public void GoHome()
    {
        GameEvents.NotifyShowConfirmQuit();
    }

    public void Config()
    {
        GameEvents.NotifyShowConfig();
    }
}
