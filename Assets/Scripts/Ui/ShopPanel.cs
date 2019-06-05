using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : UiElement
{
    public void GoHome()
    {
        GameController.Instance.GoHome();
    }

    public void Clear()
    {
        InventoryManager.Instance.Clear();
    }

    public void GetMoney()
    {
        InventoryManager.Instance.AddCoins(100);
    }
}
