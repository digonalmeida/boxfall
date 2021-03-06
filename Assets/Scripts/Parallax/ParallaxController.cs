﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : GameAgent
{
    private ParallaxElement[] _parallaxElements;
    [SerializeField]
    private float startSpeed = 1.0f;

    [SerializeField]
    private float maxSpeed = 5.0f;

    [SerializeField] 
    private int _maxSpeedLevel = 10;

    private int _currentLevel;
    private ScoringSystem _scoringSystem;
    
    private float _speed;

    protected override void Awake()
    {
        base.Awake();
        _parallaxElements = transform.GetComponentsInChildren<ParallaxElement>();
        _currentLevel = 1;
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();
        
        _speed = startSpeed;

        if (_scoringSystem == null)
        {
            _scoringSystem = GameController.Instance.ScoringSystem;
        }

        _scoringSystem.OnLevelChanged += OnLevelChanged;
        _currentLevel = 1;
        
    }

    protected override void OnGameEnded()
    {
        _currentLevel = 1;
        _scoringSystem.OnLevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        _currentLevel = _scoringSystem.CurrentLevel;
    }

    public float t;
    
    private void Update()
    {
        if (IsPaused)
        {
            return;
        }
        
        if (!isActiveAndEnabled)
        {
            return;
        }

        int currentLevelClamped = Mathf.Clamp(_currentLevel, 0, _maxSpeedLevel);
        t = Mathf.Clamp01((float)(_currentLevel -1) / (float)(_maxSpeedLevel -1));
        _speed = Mathf.Lerp(startSpeed, maxSpeed, t);
        foreach(var element in _parallaxElements)
        {
            var pos = element.gameObject.transform.position;
            
            pos += Vector3.left * _speed * Time.deltaTime * element.Ratio;
            if(pos.x <= element.EndPoint)
            {
                pos.x += element.StartPoint - element.EndPoint;
            }

            element.transform.position = pos;
        }
    }
}
