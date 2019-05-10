using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UiButton : UiElement
{
    private Button _button;
    
    public override void OnShow()
    {
        base.OnShow();
        _button.onClick.AddListener(OnClick);
    }

    public override void OnHide()
    {
        base.OnHide();
        _button.onClick.RemoveListener(OnClick);
    }

    public virtual void OnClick()
    {
        GameEvents.NotifyUiAccept();
    }
    
    protected override void Initialize()
    {
        base.Initialize();
        _button = GetComponent<Button>();
    }
}
