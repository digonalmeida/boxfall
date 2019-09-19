using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TankDataSource : ScriptableObject
{
    [SerializeField]
    private TankData _tankData;

    public TankData TankData => _tankData;
}
