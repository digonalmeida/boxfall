using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BestScoreLabel : UiElement
{
    private LevelController _levelController;
    private Text _text;
    public override void OnShow()
    {
        base.OnShow();

        if(_levelController == null)
        {
            _levelController = GameController.Instance.LevelController;
        }

        _levelController.OnScoreChanged += UpdateUI;
        UpdateUI();
    }

    public override void OnHide()
    {
        base.OnHide();
        _levelController.OnScoreChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        _text.text = _levelController.BestScore.ToString();
    }

    protected override void Initialize()
    {
        base.Initialize();
        _text = GetComponent<Text>();
    }
}
