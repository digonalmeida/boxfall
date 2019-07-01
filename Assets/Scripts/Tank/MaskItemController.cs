using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MaskItemController : GameAgent
{
    private SpriteRenderer _spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(MaskItemConfig maskItemConfig)
    {
        gameObject.SetActive(false);
        if (maskItemConfig == null)
        {
            return;
        }

        if(_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _spriteRenderer.sprite = maskItemConfig.MaskSprite;
        gameObject.SetActive(true);
    }
}
