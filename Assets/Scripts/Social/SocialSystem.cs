using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialSystem : MonoBehaviour
{
    private const string _leaderboardId = "CgkIj6GfxdcSEAIQAQ";
    public static SocialSystem Instance {get; private set;}

    public bool Authenticated { get; private set; }

    private bool _authenticating;
    private string _id;

    public event Action OnAuthenticationChanged;

    public void Awake()
    {
        Instance = this;
        Initialize();
        Authenticate();
    }

    public void Authenticate()
    {
        if(_authenticating || Authenticated)
        {
            return;
        }

        Social.localUser.Authenticate(OnAuthenticated);
    }

    
    
    public void ShowLeaderboards()
    {
        if(!Social.localUser.authenticated)
        {
            Authenticate();
            return;
        }

        Social.ShowLeaderboardUI();
    }

    public void SendScore(int score)
    {
        if(!Social.localUser.authenticated)
        {
            return;
        }

        Social.ReportScore(score, _leaderboardId, LeaderboardCallback);
    }

    private void OnAuthenticated(bool ok, string message)
    {
        Debug.LogFormat("authenticated {0} {1}", ok, message);
        Authenticated = Social.localUser.authenticated;
       // _authenticating = false;
        _id = Social.localUser.id;
        

        OnAuthenticationChanged?.Invoke();
    }

    private void LeaderboardCallback(bool ok)
    {
        Debug.Log("leaderboard: " + ok);
    }

    private void Initialize()
    {
        /*
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       // enables saving game progress.
       .EnableSavedGames()
       // registers a callback to handle game invitations received while the game is not running.
       //.WithInvitationDelegate(< callback method >)
       // registers a callback for turn based match notifications received while the
       // game is not running.
      // .WithMatchDelegate(< callback method >)
       // requests the email address of the player be available.
       // Will bring up a prompt for consent.
      // .RequestEmail()
       // requests a server auth code be generated so it can be passed to an
       //  associated back end server application and exchanged for an OAuth token.
     //  .RequestServerAuthCode(false)
       // requests an ID token be generated.  This OAuth token can be used to
       //  identify the player to other services such as Firebase.
       .RequestIdToken()
       .Build();
      
        PlayGamesPlatform.InitializeInstance(config);
         */

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }
}
