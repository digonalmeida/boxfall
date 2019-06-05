using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CoinsLabel : UiElement
{
    private InventoryManager _inventoryManager;
    private Text _text;

    protected override void Initialize()
    {
        base.Initialize();
        _inventoryManager = InventoryManager.Instance;
        _text = GetComponent<Text>();
    }

    public override void OnShow()
    {
        base.OnShow();

        _inventoryManager.OnCoinsChanged += UpdateUI;
        UpdateUI();
    }

    public override void OnHide()
    {
        base.OnHide();
        _inventoryManager.OnCoinsChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        _text.text = _inventoryManager.Coins.ToString();
    }
}
