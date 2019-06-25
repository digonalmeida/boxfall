using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public void Awake()
    {
        var uiSystem = GameController.Instance.Ui;
        var statePanels = GetComponentsInChildren<UIStatePanel>(true);
        foreach (var panel in statePanels)
        {
            panel.Setup(uiSystem);
        }
    }

    public void OnBackgroundClicked()
    {
        GameEvents.NotifyBackgroundClicked();
    }
}
