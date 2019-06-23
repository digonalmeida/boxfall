using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class ScoreUI : UiElement
{
    private ScoringSystem _scoringSystem;
    [SerializeField]
    private Text _scoreLabel = null;

    [SerializeField]
    private Text _levelLabel = null;
    
    [SerializeField]
    private Text _highScoreLabel = null;

    private int _lastBestScore = -1;

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
        if(_scoreLabel != null)
        {
            _scoreLabel.text = _scoringSystem.CurrentScore.ToString();
        }

        if(_highScoreLabel != null)
        {
            int bestScore = _scoringSystem.BestScore;
            if(_lastBestScore != bestScore)
            {
                _lastBestScore = bestScore;
                _highScoreLabel.text = bestScore.ToString();
            }
            
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        _scoringSystem = GameController.Instance.ScoringSystem;
    }
}
