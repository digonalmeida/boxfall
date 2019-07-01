using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : UiElement
{
    public ItemConfig _item = null;

    [SerializeField] 
    private Image _icon = null;
    
    [SerializeField] 
    private Text _title = null;
    
    [SerializeField] 
    private Text _description = null;
    
    [SerializeField] 
    private Text _price = null;
    
    [SerializeField] 
    private GameObject _boughtButton = null;
    
    [SerializeField] 
    private GameObject _buyButton = null;
    
    [SerializeField] 
    private Button _buyButtonInteractable = null;

    [SerializeField]
    private Button _equipButton = null;

    [SerializeField]
    private Button _unequipButton = null;

    private EquipmentSystem _equipmentSystem;

    protected override void Initialize()
    {
        base.Initialize();
        _equipmentSystem = InventoryManager.Instance.EquipmentSystem;
    }

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += OnInventoryChanged;
        InventoryManager.Instance.OnCoinsChanged += OnInventoryChanged;
        _equipmentSystem.OnEquipmentChanged += OnInventoryChanged;
        SetItem(_item);
    }

    private void OnDestroy()
    {
        InventoryManager.Instance.OnInventoryChanged -= OnInventoryChanged;
        InventoryManager.Instance.OnCoinsChanged -= OnInventoryChanged;
        _equipmentSystem.OnEquipmentChanged -= OnInventoryChanged;
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

        _title.text = item.Name;
        _icon.sprite = item.Icon;
        
        bool bought = InventoryManager.Instance.Check(item.Id);
        _buyButton.SetActive(false);
        _boughtButton.SetActive(false);
        _equipButton.gameObject.SetActive(false);
        _unequipButton.gameObject.SetActive(false);

        if (!bought)
        {
            _buyButton.SetActive(true);
            _buyButtonInteractable.interactable = InventoryManager.Instance.Coins >= _item.Price;
            _price.text = item.Price.ToString();
            _description.text = item.Description;
        }
        else
        {
            var upgrade = item.GetNextUpgrade();
            if (upgrade == null)
            {
                _boughtButton.SetActive(true);
                _description.text = item.Description;
            }
            else
            {
                _buyButton.SetActive(true);
                _price.text = upgrade.Price.ToString();
                _buyButtonInteractable.interactable = InventoryManager.Instance.Coins >= upgrade.Price;
                _description.text = item.GetNextUpgradeDescription();
            }

            if(_item.IsEquipment)
            {
                if(_equipmentSystem.CheckIsEquipped(_item))
                {
                    _unequipButton.gameObject.SetActive(true);
                }
                else
                {
                    _equipButton.gameObject.SetActive(true);
                }
            }
        }
    }
    
    public void Buy()
    {
        bool bought = InventoryManager.Instance.Check(_item.Id);
        if (!bought)
        {
            BuyItem(_item);
        }
        else
        {
            BuyNextUpgrade(_item);
        }
    }

    public void BuyItem(ItemConfig item)
    {
        if (InventoryManager.Instance.Coins < item.Price)
        {
            return;
        }
        
        InventoryManager.Instance.RemoveCoins(item.Price);
        InventoryManager.Instance.Add(item.Id);
    }

    public void BuyNextUpgrade(ItemConfig item)
    {
        var upgrade = item.GetNextUpgrade();
        if (InventoryManager.Instance.Coins < upgrade.Price)
        {
            return;
        }
        
        InventoryManager.Instance.RemoveCoins(upgrade.Price);
        InventoryManager.Instance.Add(item.GetNextUpgradeId());
    }

    public void Equip()
    {
        _equipmentSystem.EquipItem(_item);
    }

    public void Unequip()
    {
        _equipmentSystem.RemoveItem(_item);
    }
}
