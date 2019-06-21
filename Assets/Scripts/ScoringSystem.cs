using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoringSystem
{
    public int CurrentScore { get; private set; }
    public int CurrentLevel { get; private set; }
    public int BestScore { get; private set; }
    public int CurrentLevelScore { get; private set; }
    public int CurrentLevelTargetScore { get; private set; }

    public event Action OnScoreChanged;
    public event Action OnLevelChanged;
    public event Action OnLevelScoreChanged;
    public event Action OnCoinsChanged;

    public ScoringSystem(GameController gameController)
    {
        LoadBestScore();
        ResetScore();
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameEnded += OnGameEnded;
    }

    ~ScoringSystem()
    {
        GameEvents.OnGameStarted -= OnGameStarted;
        GameEvents.OnGameEnded -= OnGameEnded;
    }

    private void OnGameStarted()
    {
        ResetScore();
        LoadBestScore();
        GameEvents.OnBirdKilled += OnBirdKilled;
    }

    private void ResetScore()
    {
        CurrentScore = 0;
        CurrentLevel = 1;
        CurrentLevelScore = 0;
        CurrentLevelTargetScore = GetTargetScore(CurrentLevel);

        OnScoreChanged?.Invoke();
        OnLevelScoreChanged?.Invoke();
        OnLevelScoreChanged?.Invoke();
        OnCoinsChanged?.Invoke();
    }

    private void OnGameEnded()
    {
        GameEvents.OnBirdKilled -= OnBirdKilled;
    }

    private void OnBirdKilled()
    {
        CurrentScore++;
        OnScoreChanged?.Invoke();

        CurrentLevelScore++;

        if (CurrentLevelScore >= CurrentLevelTargetScore)
        {
            CurrentLevel++;
            CurrentLevelScore = 0;
            CurrentLevelTargetScore = GetTargetScore(CurrentLevel);
            OnLevelChanged?.Invoke();
        }

        OnLevelScoreChanged?.Invoke();

        if(CurrentScore > BestScore)
        {
            BestScore = CurrentScore;
            SaveBestScore();
        }

        InventoryManager.Instance.AddCoins(1);
        OnCoinsChanged?.Invoke();
    }

    private int GetTargetScore(int level)
    {
        return 4 + (level * 2);
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetInt("best_score", BestScore);
    }

    public void LoadBestScore()
    {
        BestScore = PlayerPrefs.GetInt("best_score", 0);
    }
}

