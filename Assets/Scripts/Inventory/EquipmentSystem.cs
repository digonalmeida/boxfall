using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem
{
    private InventoryManager _inventoryManager = null;
    public Dictionary<EquipmentSlot, string> _currentEquipment = null;

    public event Action OnEquipmentChanged;
    
    public EquipmentSystem(InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
        _currentEquipment = new Dictionary<EquipmentSlot, string>();
        Load();
    }

    public void EquipItem(ItemConfig item)
    {
        if (!CheckIsEquippable(item))
        {
            return;
        }

        _currentEquipment[item.EquipmentSlot] = item.Id;
        Save();
        
        NotifyEquipmentChanged();
    }

    public void RemoveItem(ItemConfig item)
    {
        if (!CheckIsEquipped(item))
        {
            return;
        }

        _currentEquipment[item.EquipmentSlot] = string.Empty;
    }

    public ItemConfig GetEquipment(EquipmentSlot slot)
    {
        return _inventoryManager.GetItem(GetEquippedItemId(slot));
    }

    public bool CheckIsEquippable(ItemConfig item)
    {
        if (item == null ||
            string.IsNullOrEmpty(item.Id) ||
            item.EquipmentSlot == EquipmentSlot.None)
        {
            return false;
        }

        return true;
    }

    public bool CheckIsEquipped(ItemConfig item)
    {
        if (!CheckIsEquippable(item))
        {
            return false;
        }

        return item.Id == GetEquippedItemId(item.EquipmentSlot);
    }

    public string GetEquippedItemId(EquipmentSlot slot)
    {
        if (slot == EquipmentSlot.None)
        {
            return string.Empty;
        }
        
        string id;
        _currentEquipment.TryGetValue(slot, out id);
        return id;
    }

    private void Save()
    {
        EquipmentItemData[] itemData = new EquipmentItemData[_currentEquipment.Keys.Count];
        int i = 0;
        foreach (var equipmentEntry in _currentEquipment)
        {
            itemData[i] = new EquipmentItemData(equipmentEntry.Key, equipmentEntry.Value);
            i++;
        }
        
        var equipmentData = new EquipmentData(itemData);
        var json = JsonUtility.ToJson(equipmentData);
        PlayerPrefs.SetString("equipment", json);
    }

    private void Load()
    {
        var json = PlayerPrefs.GetString("equipment");
        _currentEquipment.Clear();
        
        if (string.IsNullOrEmpty(json))
        {
            return;
        }
        
        var equipmentData = JsonUtility.FromJson<EquipmentData>(json);

        if (equipmentData.Items == null)
        {
            return;
        }
        
        
        foreach (var equipmentItem in equipmentData.Items)
        {
            _currentEquipment[equipmentItem.Slot] = equipmentItem.Id;
        }
    }

    private void NotifyEquipmentChanged()
    {
        OnEquipmentChanged?.Invoke();
    }

    [Serializable]
    private class EquipmentData
    {
        public readonly EquipmentItemData[] Items;

        public EquipmentData(EquipmentItemData[] items)
        {
            Items = items;
        }
    }
    
    [Serializable]
    private class EquipmentItemData
    {
        public readonly string Id;
        public readonly EquipmentSlot Slot;

        public EquipmentItemData(EquipmentSlot slot, string id)
        {
            Slot = slot;
            Id = id;
        }
    }
}
