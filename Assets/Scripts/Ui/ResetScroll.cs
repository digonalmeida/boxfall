using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ResetScroll : UiElement
{
    private ScrollRect scrollRect;

    protected override void Initialize()
    {
        base.Initialize();
        scrollRect = GetComponent<ScrollRect>();
    }

    public override void OnShow()
    {
        base.OnShow();
        scrollRect.normalizedPosition = Vector2.up;
    }
}
