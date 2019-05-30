using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CoinsLabel : UiElement
{
    private LevelController _levelController;
    private Text _text;

    protected override void Initialize()
    {
        base.Initialize();
        _levelController = GameController.Instance.LevelController;
        _text = GetComponent<Text>();
    }

    public override void OnShow()
    {
        base.OnShow();

        _levelController.OnCoinsChanged += UpdateUI;
        UpdateUI();
    }

    public override void OnHide()
    {
        base.OnHide();
        _levelController.OnCoinsChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        _text.text = _levelController.Coins.ToString();
    }
}
