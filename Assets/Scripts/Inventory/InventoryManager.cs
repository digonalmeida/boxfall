using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private ItemConfig[] AllItems;

    public static InventoryManager Instance { get; private set; }
    
    public event Action OnCoinsChanged; 
    public event Action OnInventoryChanged; 
    
    private List<string> _inventory;
    
    public int Coins { get; private set; }

    public float GetStarPowerupDuration()
    {
        var items = GetItems<StarUpgradeConfig>();
        float val = 0;
        foreach(var item in items)
        {
            if(item.Duration > val)
            {
                val = item.Duration;
            }
        }

        return val;
    }
    
    public List<T> GetItems<T>() where T:ItemConfig
    {
        var list = new List<T>();
        foreach(var item in AllItems)
        {
            if(item is T)
            {
                list.Add(item as T);
            }
        }
        return list;
    }

    public ItemConfig GetItem(string id)
    {
        foreach(var item in AllItems)
        {
            if(item.Id == id)
            {
                return item;
            }
        }

        return null;
    }

    public void Clear()
    {
        _inventory.Clear();
        Coins = 0;
        OnCoinsChanged?.Invoke();
        OnInventoryChanged?.Invoke();
        Save();
    }

    public void Add(string itemId)
    {
        if (Check(itemId))
        {
            return;
        }
        _inventory.Add(itemId);
        OnInventoryChanged?.Invoke();
        Save();
    }

    public bool Check(string itemId)
    {
        return _inventory.Contains(itemId);
    }

    public void AddCoins(int count)
    {
        Coins += count;
        OnCoinsChanged?.Invoke();
        SaveCoins();
    }

    public void RemoveCoins(int count)
    {
        Coins -= count;
        OnCoinsChanged?.Invoke();
        SaveCoins();
    }
    
    private void Awake()
    {
        Load();
        Instance = this;
    }
    
    private void Save()
    {
        var data = new InventoryData(_inventory);
        var jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("inventory", jsonData);
        SaveCoins();
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("coins", Coins);
    }

    private void Load()
    {
        Coins = PlayerPrefs.GetInt("coins", 0);
        
        var jsonData = PlayerPrefs.GetString("inventory", "");
        if (string.IsNullOrEmpty(jsonData))
        {
            _inventory = new List<string>();
            return;
        }
        
        var data = JsonUtility.FromJson<InventoryData>(jsonData);
        _inventory = data.Items;
    }

    private struct InventoryData
    {
        [SerializeField]
        private string[] _items;

        public List<string> Items
        {
            get
            {
                return new List<string>(_items);
            }
        }

        public InventoryData(List<string> items)
        {
            _items = items.ToArray();
        }
    }
    
}
