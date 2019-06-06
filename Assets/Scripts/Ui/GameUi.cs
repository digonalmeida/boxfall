using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    [SerializeField] 
    private UiElement _homeScreenPanel = null;
        
    [SerializeField]
    private UiElement _inGameCanvas = null;

    [SerializeField]
    private UiElement _endGameCanvas = null;

    [SerializeField] 
    private UiElement _pauseGameCanvas = null;

    [SerializeField]
    private UiElement _shopCanvas = null;
    
    private void Awake()
    {
        HideAll();
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameEnded += OnGameEnded;
        GameEvents.OnGamePaused += OnGamePaused;
        GameEvents.OnGameUnpaused += OnGameUnpaused;
        GameEvents.OnShowHomeScreen += OnShowHomeScreen;
        GameEvents.OnShowShop += OnShowShop;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= OnGameStarted;
        GameEvents.OnGameEnded -= OnGameEnded;
        GameEvents.OnGamePaused -= OnGamePaused;
        GameEvents.OnGameUnpaused -= OnGameUnpaused;
        GameEvents.OnShowHomeScreen -= OnShowHomeScreen;
        GameEvents.OnShowShop -= OnShowShop;
    }

    private void OnShowHomeScreen()
    {
        HideAll();
        Show(_homeScreenPanel);
    }
    
    private void OnGameStarted()
    {
        HideAll();
        Show(_inGameCanvas);
    }

    private void OnGameEnded()
    {
        HideAll();
        Show(_endGameCanvas);
    }

    private void OnGamePaused()
    {
        Show(_pauseGameCanvas);
    }

    private void OnGameUnpaused()
    {
        Hide(_pauseGameCanvas);
    }

    private void OnShowShop()
    {
        HideAll();
        Show(_shopCanvas);
    }

    private void HideAll()
    {
        Hide(_homeScreenPanel);
        Hide(_inGameCanvas);
        Hide(_endGameCanvas);
        Hide(_pauseGameCanvas);
        Hide(_shopCanvas);
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

    public void OnBackgroundClicked()
    {
        GameEvents.NotifyBackgroundClicked();
    }
}
