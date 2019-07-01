using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEquipmentController : TankComponent
{
    [SerializeField]
    private HatItemController _hatItemObject = null;

    [SerializeField]
    private MaskItemController _maskItemObject = null;

    private EquipmentSystem _equipmentSystem = null;

    public override void Initialize(TankController tank)
    {
        base.Initialize(tank);
        _equipmentSystem = InventoryManager.Instance.EquipmentSystem;
        _equipmentSystem.OnEquipmentChanged += OnEquipmentChanged;
        UpdateEquipment();
    }

    private void OnDestroy()
    {
        if(_equipmentSystem != null)
        {
            _equipmentSystem.OnEquipmentChanged -= OnEquipmentChanged;
        }
    }

    public void OnEquipmentChanged()
    {
        UpdateEquipment();
    }

    public void UpdateEquipment()
    {
        var hat = _equipmentSystem.GetEquipment(EquipmentSlot.Hat);
        var mask = _equipmentSystem.GetEquipment(EquipmentSlot.Mask);
        
        _hatItemObject.SetItem(hat);
        _maskItemObject.SetItem(mask as MaskItemConfig);
    }

    public bool TakeDamage()
    {
        return _hatItemObject.TakeDamage();
    }

    
}
