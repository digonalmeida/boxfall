using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmQuitPanel : UIStatePanel
{
    public ConfirmQuitPanel() 
        : base(EUiState.ConfirmQuit)
    {
        //
    }

    public void GoHome()
    {
        GameController.Instance.GoHome();
    }
}
