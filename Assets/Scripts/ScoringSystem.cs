using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoringSystem
{
    public int CurrentScore { get; private set; }
    public int CurrentLevel { get; private set; }
    public int BestScore { get; private set; }
    public int BestLevel { get; private set; }
    public int CurrentLevelScore { get; private set; }
    public int CurrentLevelTargetScore { get; private set; }

    public event Action OnScoreChanged;
    public event Action OnLevelChanged;
    public event Action OnLevelScoreChanged;

    public ScoringSystem(GameController gameController)
    {
        LoadBestScore();
        LoadBestLevel();
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
        LoadBestLevel();
        GameEvents.OnBirdKilled += OnBirdKilled;
    }

    private void ResetScore()
    {
        CurrentScore = 0;
        CurrentLevel = 1;
        CurrentLevelScore = 0;
        BestLevel = 1;
        CurrentLevelTargetScore = GetTargetScore(CurrentLevel);

        OnScoreChanged?.Invoke();
        OnLevelScoreChanged?.Invoke();
        OnLevelScoreChanged?.Invoke();
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

        if(CurrentLevel > BestLevel)
        {
            BestLevel = CurrentLevel;
            SaveBestLevel();
        }

        InventoryManager.Instance.AddCoins(1);
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

    public void SaveBestLevel()
    {
        PlayerPrefs.SetInt("best_level", BestLevel);
    }

    public void LoadBestLevel()
    {
        BestLevel = PlayerPrefs.GetInt("best_level", 1);
    }
}

