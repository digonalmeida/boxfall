using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShieldPowerupUI : UiElement
{
    [SerializeField]
    private Image _progressBar;

    private PowerUpsManager _powerUpsManager;
    private PowerUpData _powerUpData;

    protected override void Initialize()
    {
        base.Initialize();
        _powerUpsManager = GameController.Instance.PowerUpsManager;
    }

    public override void OnShow()
    {
        base.OnShow();
        
        _powerUpsManager.OnActivatePowerUp += OnActivatePowerUp;
        _powerUpsManager.OnDeactivatePowerUp += OnDeactivatePowerUp;
        
        _progressBar.fillAmount = 0;
    }

    public override void OnHide()
    {
        base.OnHide();

        _powerUpsManager.OnActivatePowerUp -= OnActivatePowerUp;
        _powerUpsManager.OnDeactivatePowerUp -= OnDeactivatePowerUp;
    }

    private void OnActivatePowerUp(PowerUpData powerUp)
    {
        _progressBar.gameObject.SetActive(true);
        _powerUpData = powerUp;
    }

    private void OnDeactivatePowerUp(PowerUpData powerUp)
    {
        _progressBar.gameObject.SetActive(false);
        _powerUpData = null;
    }

    private void UpdateBar()
    {
        _progressBar.fillAmount = _powerUpData.Time / _powerUpData.TotalTime;
    }

    private void Update()
    {
        if(_powerUpData == null)
        {
            return;
        }

        UpdateBar();
    }
}
