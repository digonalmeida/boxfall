using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShieldPowerupUI : MonoBehaviour
{
    [SerializeField]
    private Image _progressBar;

    private PowerUpData _powerUpData;

    private void Awake()
    {
        GameEvents.OnActivatePowerUp += OnActivatePowerUp;
        GameEvents.OnDeactivatePowerUp += OnDeactivatePowerUp;
    }

    private void OnDestroy()
    {
        GameEvents.OnActivatePowerUp -= OnActivatePowerUp;
        GameEvents.OnDeactivatePowerUp -= OnDeactivatePowerUp;
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

    // Update is called once per frame
    void Update()
    {
        if(_powerUpData == null)
        {
            return;
        }

        UpdateBar();
    }
}
