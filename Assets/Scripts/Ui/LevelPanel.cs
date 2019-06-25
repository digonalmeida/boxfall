using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : UiElement
{
    [SerializeField]
    private Image _levelProgress = null;

    [SerializeField]
    private Text _levelText = null;

    private ScoringSystem _scoringSystem;

    protected override void Initialize()
    {
        base.Initialize();
        _scoringSystem = GameController.Instance.ScoringSystem;
    }

    public override void OnShow()
    {
        base.OnShow();
        _scoringSystem.OnLevelScoreChanged += UpdateUi;
        UpdateUi();
    }

    public override void OnHide()
    {
        base.OnHide();
        _scoringSystem.OnLevelScoreChanged -= UpdateUi;
    }

    private void UpdateUi()
    {
        var total = _scoringSystem.CurrentLevelTargetScore;
        var current = _scoringSystem.CurrentLevelScore;
        _levelProgress.fillAmount = (float)current / (float)total;
        _levelText.text = _scoringSystem.CurrentLevel.ToString();
    }
}
