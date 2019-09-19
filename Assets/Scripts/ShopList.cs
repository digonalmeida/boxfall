using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : UiElement
{
    [SerializeField] 
    private ShopItemController _template = null;

    [SerializeField] 
    public List<ItemConfig> _items = null;

    private List<ShopItemController> _instantiatedShopItems = new List<ShopItemController>();

    protected override void Initialize()
    {
        base.Initialize();
        _items = InventoryManager.Instance.GetShopItems();
        CreateList();
    }
    
    public void CreateList()
    {
        _template.gameObject.SetActive(false);
        foreach (var item in _instantiatedShopItems)
        {
            Destroy(item.gameObject);
        }
        
        _instantiatedShopItems.Clear();
        
        foreach (var item in _items)
        {
            CreateItem(item);
        }
    }

    public void CreateItem(ItemConfig config)
    {
        var item = Instantiate(_template.gameObject, _template.transform.parent).GetComponent<ShopItemController>();
        item.transform.SetAsLastSibling();
        item.gameObject.SetActive(true);
        item.SetItem(config);
        _instantiatedShopItems.Add(item);
    }
}
