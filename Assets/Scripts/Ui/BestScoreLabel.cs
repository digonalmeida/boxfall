using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BestScoreLabel : UiElement
{
    private ScoringSystem _scoringSystem;
    private Text _text;
    public override void OnShow()
    {
        base.OnShow();

        if(_scoringSystem == null)
        {
            _scoringSystem = GameController.Instance.ScoringSystem;
        }

        _scoringSystem.OnScoreChanged += UpdateUI;
        UpdateUI();
    }

    public override void OnHide()
    {
        base.OnHide();
        _scoringSystem.OnScoreChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        _text.text = _scoringSystem.BestScore.ToString();
    }

    protected override void Initialize()
    {
        base.Initialize();
        _text = GetComponent<Text>();
    }
}
