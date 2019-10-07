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

    private EquipmentSystem _equipmentSystem;

    public ScoringSystem(GameController gameController, EquipmentSystem equipmentSystem)
    {
        LoadBestScore();
        LoadBestLevel();
        ResetScore();
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameEnded += OnGameEnded;

        _equipmentSystem = equipmentSystem;
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

    private void OnBirdKilled(BirdController bird)
    {
        int score = CalculateScore(bird);

        CurrentScore += score;
        OnScoreChanged?.Invoke();

        CurrentLevelScore += score;

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

        InventoryManager.Instance.AddCoins(score);
    }

    private int CalculateScore(BirdController bird)
    {
        float score = 1;
        
        var equippedMask = _equipmentSystem.GetEquipment(EquipmentSlot.Mask) as MaskItemConfig;
        if(equippedMask != null && equippedMask.BirdColor == bird.BirdColor)
        {
            score *= equippedMask.GetCurrentValue();
        }

        return Mathf.CeilToInt(score);
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

