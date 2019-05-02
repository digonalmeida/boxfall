using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private GameEntity _entity;

    [SerializeField]
    private GameObject _scoreCanvas;

    [SerializeField]
    private Text _scoreTextUi;

    [SerializeField]
    private GameObject _gameOverCanvas;

    private void Awake()
    {
        _entity = GetComponent<GameEntity>();

    }

    private void OnDestroy()
    {
        
    }

    public void UpdateInGameUI()
    {
        _scoreCanvas.SetActive(true);
        _scoreTextUi.text = _entity.CurrentScore.ToString();
        _gameOverCanvas.SetActive(false);
    }

    public void UpdateEndGameUI()
    {
        _gameOverCanvas.SetActive(true);
    }

}
