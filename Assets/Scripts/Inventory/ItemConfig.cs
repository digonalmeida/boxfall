using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    None,
    Mask,
    Hat
}

[CreateAssetMenu]
public class ItemConfig : ScriptableObject
{
    [SerializeField] 
    private string _id = "";
    
    [SerializeField]
    private string _name = "";

    [SerializeField] 
    private int _price = 1;

    [SerializeField] 
    private Sprite _icon = null;

    [SerializeField] 
    private string _description = string.Empty;

    [SerializeField]
    private string _upgradeDescription = string.Empty;
    
    [SerializeField] 
    private bool _isInitial = false;

    [SerializeField] 
    private bool _showOnShop = true;

    [SerializeField] 
    private EquipmentSlot _equipmentSlot;

    [SerializeField] 
    private float _baseValue;
    
    [SerializeField] 
    public ItemUpgradeConfig[] _upgrades;

    public string Name
    {
        get { return _name; }
    }

    public string Id
    {
        get { return _id; }
    }

    public int Price
    {
        get { return _price; }
    }

    public Sprite Icon
    {
        get { return _icon; }
    }

    public virtual string Description
    {
        get { return string.Format(_description, _baseValue); }
    }

    public bool IsInitial
    {
        get { return _isInitial; }
    }

    public bool ShowOnShop
    {
        get { return _showOnShop; }
    }

    public EquipmentSlot EquipmentSlot
    {
        get { return _equipmentSlot; }
    }

    public string GetUpgradeId(int id)
    {
        return string.Format("{0}_upgrade_{1}", _id, id);
    }
    
    private int GetNextUpgradeIndex()
    {
        for (int i = 0; i < _upgrades.Length; i++)
        {
            string upgradeId = GetUpgradeId(i);
            if (InventoryManager.Instance.Check(upgradeId))
            {
                continue;
            }

            return i;
        }

        return -1;
    }
    
    private int GetCurrentUpgradeIndex()
    {
        int i = 0;
        for (i = 0; i < _upgrades.Length; i++)
        {
            string upgradeId = GetUpgradeId(i);
            if (!InventoryManager.Instance.Check(upgradeId))
            {
                return i - 1;
            }
        }

        return i;
    }
    
    public ItemUpgradeConfig GetNextUpgrade()
    {
        int index = GetNextUpgradeIndex();
        if (index == -1)
        {
            return null;
        }

        return _upgrades[index];
    }

    public string GetNextUpgradeId()
    {
        int index = GetNextUpgradeIndex();
        if (index == -1)
        {
            return null;
        }

        return GetUpgradeId(index);
    }

    public string GetNextUpgradeDescription()
    {
        var upgrade = GetNextUpgrade();
        if (upgrade == null)
        {
            return _description;
        }

        return string.Format(_upgradeDescription, upgrade.Value);
    }

    public ItemUpgradeConfig GetCurrentUpgrade()
    {
        var index = GetCurrentUpgradeIndex();
        if (index == -1)
        {
            return null;
        }

        return _upgrades[index];
    }
    
    public float GetCurrentValue()
    {
        return GetCurrentUpgrade()?.Value ?? _baseValue;
    }
}
