using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmQuitPanel : UiElement
{
    public void Close()
    {
        Hide();
    }

    public void GoHome()
    {
        GameController.Instance.GoHome();
    }
}
