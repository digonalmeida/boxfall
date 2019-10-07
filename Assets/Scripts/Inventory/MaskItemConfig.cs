using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu]
public class MaskItemConfig : ItemConfig
{
    [SerializeField]
    private Sprite _maskSprite;

    [FormerlySerializedAs("_birdType")] [SerializeField]
    public BirdColor birdColor;

    public Sprite MaskSprite => _maskSprite;
    public BirdColor BirdColor => birdColor;
}
