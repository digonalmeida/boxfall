using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StarPowerupConfig : ItemConfig
{
    [SerializeField] 
    private float _baseDuration = 5.0f;

    [SerializeField] private string _maxedDescription = "";


}
