using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    [SerializeField] 
    private UiElement _startGameCanvas = null;
        
    [SerializeField]
    private UiElement _inGameCanvas = null;

    [SerializeField]
    private UiElement _endGameCanvas = null;

    private void Awake()
    {
        HideAll();
    }

    private void HideAll()
    {
        Hide(_startGameCanvas);
        Hide(_inGameCanvas);
        Hide(_endGameCanvas);
    }
    
    public void ShowInGameUi()
    {
        Show(_inGameCanvas);
    }

    public void HideInGameUi()
    {
        Hide(_inGameCanvas);
    }
    
    public void ShowEndGameUi()
    {
        Show(_endGameCanvas);
    }

    public void HideEndGameUi()
    {
        Hide(_endGameCanvas);
    }

    public void ShowStartGameUi()
    {
        Show(_startGameCanvas);
    }

    public void HideStartGameUi()
    {
        Hide(_startGameCanvas);
    }

    private void Show(UiElement uiElement)
    {
        if(uiElement == null)
        {
            return;
        }

        uiElement.Show();
    }

    private void Hide(UiElement uiElement)
    {
        if(uiElement == null)
        {
            return;
        }

        uiElement.Hide();
    }
}
