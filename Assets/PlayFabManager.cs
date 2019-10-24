using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance { get; private set; }
    
    public Dictionary<string, string> TitleData;
    public event Action OnDataUpdated;
    
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
#if !UNITY_ANDROID || UNITY_EDITOR
        RequestLogin();
#endif
#if UNITY_ANDROID && UNITY_EDITOR
        SocialSystem.Instance.OnAuthenticationChanged += OnSocialLoginChanged;
#endif
    }

    private void OnSocialLoginChanged()
    {
        RequestGoogleLogin();
    }

    private void RequestGoogleLogin()
    {
        
    }

    private void RequestLogin()
    {
        var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("PlayFab Login Success");
        RequestTitleData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("PlayFab Login Failed");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void RequestTitleData()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            return;
        }
        
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
        
        OnDataUpdated?.Invoke();
    }

    public string GetTitleData(string sourcePropetyName)
    {
        TitleData.TryGetValue(sourcePropetyName, out string data);
        return data;
    }
}
