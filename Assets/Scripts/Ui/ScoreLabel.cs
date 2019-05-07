using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreLabel : UiElement
{
    private Text _text;
    public override void OnShow()
    {
        base.OnShow();
        GameEvents.OnScoreChanged += UpdateUI;
        UpdateUI();
    }

    public override void OnHide()
    {
        base.OnHide();
        GameEvents.OnScoreChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        _text.text = GameEntity.Instance.CurrentScore.ToString();
    }

    protected override void Initialize()
    {
        base.Initialize();
        _text = GetComponent<Text>();
    }
}
