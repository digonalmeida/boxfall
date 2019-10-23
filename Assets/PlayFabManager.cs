using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Dictionary<string, string> TitleData;

    public void Start()
    {
        RequestLogin();
    }

    private void RequestLogin()
    {
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        LoginWithAndroidDeviceIDRequest request = new LoginWithAndroidDeviceIDRequest();
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        PlayFabClientAPI.LoginWithAndroidDeviceID();
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        RequestTitleData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    private void RequestTitleData()
    {
        GetTitleDataRequest titleDataRequest = new GetTitleDataRequest();
        PlayFabClientAPI.GetTitleData(titleDataRequest, OnGetTitleDataSuccess, OnGetTitleDataError);
    }

    private void OnGetTitleDataError(PlayFabError obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnGetTitleDataSuccess(GetTitleDataResult obj)
    {
        TitleData = obj.Data;
        Debug.Log("data received");
        foreach (var pair in TitleData)
        {
            Debug.Log($"{pair.Key}:{pair.Value}");
        }
    }
}
