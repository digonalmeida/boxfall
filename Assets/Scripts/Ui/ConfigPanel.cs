using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Button = UnityEngine.UI.Button;

public class ConfigPanel : UIStatePanel
{
    [SerializeField] 
    private GameObject _soundOnButton = null;
    
    [SerializeField] 
    private GameObject _soundOffButton = null;
    
    [SerializeField]
    private Button _socialLoginButton = null;
    
    //TODO implement options
    private static bool _soundOn = true;
    
    //TODO implement options
    public static bool _socialLogin = true;
    
    public ConfigPanel() 
        : base(EUiState.ConfigPanel)
    {
        //
    }

    public override void OnShow()
    {
        _socialLogin = true;
        UpdateUi();
    }

    public void ToggleSound()
    {
        _soundOn = !_soundOn;
        UpdateUi();
    }

    public void ToggleLogin()
    {
        _socialLogin = !_socialLogin;
        UpdateUi();
    }

    private void UpdateUi()
    {
        _soundOnButton.SetActive(_soundOn);
        _soundOffButton.SetActive(!_soundOn);
        _socialLoginButton.interactable = _socialLogin;
    }
}
