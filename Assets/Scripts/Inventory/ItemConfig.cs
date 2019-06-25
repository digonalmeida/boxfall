using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemConfig : ScriptableObject
{
    [SerializeField] 
    private string _id;
    
    [SerializeField]
    private string _name;

    [SerializeField] 
    private int _price;

    [SerializeField] 
    private Sprite _icon;

    [SerializeField] 
    private string _description = string.Empty;

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
        get { return _description; }
    }
}
