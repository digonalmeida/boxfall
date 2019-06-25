using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndGamePanel : UIStatePanel
{
    private ScoringSystem _scoringSystem;
    private InventoryManager _inventoryManager;

    [SerializeField]
    private Text _lastScore = null;

    [SerializeField]
    private Text _bestScore = null;

    [SerializeField]
    private Text _lastLevel = null;

    [SerializeField]
    private Text _bestLevel = null;

    [SerializeField]
    private Text _currency = null;

    public EndGamePanel() 
        : base(EUiState.EndGame)
    {
        //
    }
    
    public void GoHome()
    {
        GameController.Instance.GoHome();
    }

    public void PlayAgain()
    {
        GameController.Instance.StartGame();
    }

    public override void OnShow()
    {
        base.OnShow();
        UpdateUi();
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    protected override void Initialize()
    {
        base.Initialize();
        _scoringSystem = GameController.Instance.ScoringSystem;
        _inventoryManager = InventoryManager.Instance;
    }

    private void UpdateUi()
    {
        _lastScore.text = _scoringSystem.CurrentLevelScore.ToString();
        _bestScore.text = _scoringSystem.BestScore.ToString();
        _lastLevel.text = _scoringSystem.CurrentLevel.ToString();
        _bestLevel.text = _scoringSystem.BestLevel.ToString();
        _currency.text = _inventoryManager.Coins.ToString();
    }
}
