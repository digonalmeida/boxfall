using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : UiElement
{
    public ItemConfig _item = null;

    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;
    [SerializeField] private Text _description;
    [SerializeField] private Text _price;
    [SerializeField] private GameObject _unavailableButton;
    [SerializeField] private GameObject _boughtButton;
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private Button _buyButtonInteractable;
    [SerializeField] private Text _upgradeDescription;

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += OnInventoryChanged;
        InventoryManager.Instance.OnCoinsChanged += OnInventoryChanged;
        SetItem(_item);
    }

    private void OnDestroy()
    {
        InventoryManager.Instance.OnInventoryChanged -= OnInventoryChanged;
        InventoryManager.Instance.OnCoinsChanged -= OnInventoryChanged;
    }

    private void OnInventoryChanged()
    {
        if (_item == null)
        {
            return;
        }
        
        SetItem(_item);
    }

    public void SetItem(ItemConfig item)
    {
        _item = item;

        if (item is StarPowerupConfig)
        {
            SetupStarPowerup((StarPowerupConfig)item);
        }
    }

    private void SetupStarPowerup(StarPowerupConfig item)
    {
        _item = item;
        
        _title.text = item.Name;
        _item = item;
        _icon.sprite = item.Icon;
        _description.text = item.Description;
        
        _unavailableButton.SetActive(false);
        
        var upgrade = item.GetNextUpgrade();
        if (upgrade == null)
        {
            _buyButton.SetActive(false);
            _boughtButton.SetActive(true);
        }
        else
        {
            _buyButton.SetActive(true);
            _boughtButton.SetActive(false);
            _price.text = upgrade.Price.ToString();
            _buyButtonInteractable.interactable = InventoryManager.Instance.Coins >= _item.Price;
            _upgradeDescription.text = upgrade.Description;
        }
    }

    public void Buy()
    {
        var starPowerup =  _item as StarPowerupConfig;
        if (starPowerup == null)
        {
            return;
        }
        
        var upgrade = starPowerup.GetNextUpgrade();
        
        if (upgrade == null)
        {
            return;
        }

        if (InventoryManager.Instance.Coins < upgrade.Price)
        {
            return;
        }
        
        InventoryManager.Instance.RemoveCoins(upgrade.Price);
        InventoryManager.Instance.Add(upgrade.Id);
        
    }
}
