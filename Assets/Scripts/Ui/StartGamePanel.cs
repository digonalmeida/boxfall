using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGamePanel : UiElement
{
    public void ShowShop()
    {
        GameEvents.NotifyShowShop();
    }
}
