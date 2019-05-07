using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    [SerializeField] 
    private UiElement _startGameCanvas;
        
    [SerializeField]
    private UiElement _inGameCanvas;

    [SerializeField]
    private UiElement _endGameCanvas;

    private void Awake()
    {
        HideAll();
    }

    private void HideAll()
    {
        _startGameCanvas.Hide();
        _inGameCanvas.Hide();
        _endGameCanvas.Hide();
    }
    
    public void ShowInGameUi()
    {
        _inGameCanvas.Show();
    }

    public void HideInGameUi()
    {
        _inGameCanvas.Hide();
    }
    
    public void ShowEndGameUi()
    {
        _endGameCanvas.Show();
    }

    public void HideEndGameUi()
    {
        _endGameCanvas.Hide();
    }

    public void ShowStartGameUi()
    {
        _startGameCanvas.Show();
    }

    public void HideStartGameUi()
    {
        _startGameCanvas.Hide();
    }
}
