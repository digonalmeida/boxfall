using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : UiElement
{
    [SerializeField]
    private Image _levelProgress;
    [SerializeField]
    private Text _levelText;

    private LevelController _levelController;

    protected override void Initialize()
    {
        base.Initialize();
        _levelController = GameController.Instance.LevelController;
    }

    public override void OnShow()
    {
        base.OnShow();
        _levelController.OnLevelScoreChanged += UpdateUi;
        UpdateUi();
    }

    public override void OnHide()
    {
        base.OnHide();
        _levelController.OnLevelScoreChanged -= UpdateUi;
    }

    private void UpdateUi()
    {
        var total = _levelController.CurrentLevelTargetScore;
        var current = _levelController.CurrentLevelScore;
        _levelProgress.fillAmount = (float)current / (float)total;
        _levelText.text = _levelController.CurrentLevel.ToString();
    }
}
