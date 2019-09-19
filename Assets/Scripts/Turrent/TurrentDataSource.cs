using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurrentDataSource : ScriptableObject
{
    [SerializeField]
    private TurrentData _turrentData;

    public TurrentData TurrentData => _turrentData;
}
