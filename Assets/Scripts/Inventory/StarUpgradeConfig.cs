using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StarUpgradeConfig : ItemConfig
{
    [Header("Star Configuration")]
    [SerializeField] 
    private float _duration;

    public float Duration
    {
        get { return _duration; }
    }

    public override string Description
    {
        get { return string.Format(base.Description, _duration); }
    }
}
