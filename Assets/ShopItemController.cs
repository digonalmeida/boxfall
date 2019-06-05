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

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += OnInventoryChanged;
        InventoryManager.Instance.OnCoinsChanged += OnInventoryChanged;
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
        _icon.sprite = item.Icon;
        _title.text = item.Name;
        _description.text = item.Description;
        _price.text = item.Price.ToString();
        _unavailableButton.SetActive(false);
        _boughtButton.SetActive(false);
        _buyButton.SetActive(false);
        _buyButtonInteractable.interactable = InventoryManager.Instance.Coins >= _item.Price;
        
        if (InventoryManager.Instance.Check(_item.Id))
        {
            _boughtButton.SetActive(true);
            return;
        }

        foreach (var itemRequiredItem in _item.RequiredItems)
        {
            if (!InventoryManager.Instance.Check(itemRequiredItem.Id))
            {
                _unavailableButton.SetActive(true);
                return;
            }
        }
        
        _buyButton.SetActive(true);
    }

    public void Buy()
    {
        if (InventoryManager.Instance.Coins < _item.Price)
        {
            return;
        }
        
        InventoryManager.Instance.RemoveCoins(_item.Price);
        InventoryManager.Instance.Add(_item.Id);
    }
}
