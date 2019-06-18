using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGamePanel : UiElement
{
    public void ShowShop()
    {
        GameEvents.NotifyShowShop();
    }
    
    public void Config()
    {
        GameEvents.NotifyShowConfig();
    }

    public void Leaderboards()
    {
        Debug.Log("Show leaderboards");
    }
}
