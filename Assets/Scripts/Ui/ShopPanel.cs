using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : UIStatePanel
{
    public ShopPanel() 
        : base(EUiState.Shop)
    {
        //
    }
    
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
        InventoryManager.Instance.AddCoins(1000);
    }
}
