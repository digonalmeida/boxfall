using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StarPowerupConfig : ItemConfig
{
    [SerializeField] 
    private StarUpgradeConfig[] _allUpgrades = null;

    [SerializeField] 
    private float _baseDuration = 5.0f;

    public override string Description
    {
        get
        {
            return string.Format(base.Description, GetDuration());
        }
    }

    public float GetDuration()
    {
        float duration = _baseDuration;

        foreach (var upgrade in _allUpgrades)
        {
            if (!InventoryManager.Instance.Check(upgrade.Id))
            {
                continue;
            }
            
            duration += upgrade.Duration;
        }

        return duration;
    }

    public StarUpgradeConfig GetNextUpgrade()
    {
        foreach (var upgrade in _allUpgrades)
        {
            if (InventoryManager.Instance.Check(upgrade.Id))
            {
                continue;
            }

            return upgrade;
        }

        return null;
    }
}
