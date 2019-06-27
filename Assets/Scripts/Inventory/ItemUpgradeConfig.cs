using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemUpgradeConfig
{
    [SerializeField]
    public int _price;
    
    [SerializeField]
    public float _value;

    public int Price
    {
        get { return _price; }
    }

    public float Value
    {
        get { return _value; }
    }
}
