using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : UiElement
{
    public void GoHome()
    {
        GameController.Instance.GoHome();
    }

    public void PlayAgain()
    {
        GameController.Instance.StartGame();
    }
}
