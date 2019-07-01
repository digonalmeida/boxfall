using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaskItemBirdType
{
    BLUE,
    PINK,
    YELLOW,
    BLACK,
    RED
}
[CreateAssetMenu]
public class MaskItemConfig : ItemConfig
{
    [SerializeField]
    private Sprite _maskSprite;

    [SerializeField]
    public MaskItemBirdType _birdType;

    public Sprite MaskSprite => _maskSprite;
    public MaskItemBirdType BirdType => _birdType;
}
