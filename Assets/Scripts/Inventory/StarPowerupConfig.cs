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

    [SerializeField] private string _maxedDescription;

    public override string Description
    {
        get
        {
            var duration = GetDuration();
            var nextUpgrade = GetNextUpgrade();

            if (nextUpgrade == null)
            {
                return string.Format(_maxedDescription, duration);
            }
            
            var nextDuration = duration + nextUpgrade.Duration;
            return string.Format(base.Description, duration, nextDuration);
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
