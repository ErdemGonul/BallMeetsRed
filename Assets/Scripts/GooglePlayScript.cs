using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class GooglePlayScript : MonoBehaviour {

    public GameObject text;
    public static GooglePlayScript instance;
    private BannerView banner;
    public GameObject notifyPanel;
    public GameObject startPanel;
    // Use this for initialization
    private void Awake()
    {
        
        
    }
    void Start()
    {
       
       
    }
    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void signIn()
    {

        Social.localUser.Authenticate((bool success)
            =>
        {
            if (success)
            {
                notifyPanel.SetActive(false);
                GooglePlayScript.ShowLeaderBoardUI();
                Debug.Log("bak bu giriş");
            }
            else
            {
                notifyPanel.SetActive(false);
            }
 
        }
            );
       
    }
	// Update is called once per frame
	void Update () {
		
	}
    #region     LeaderBoards

    public static void AddScoreToLeaderBoard(string leaderboardID)
    {
        if(Social.localUser.authenticated)
        Social.ReportScore(PlayerPrefs.GetInt("best"), leaderboardID,success => { });
        else
        {

        }
    }
    public void connectIt()
    {
        if ( (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork))
        {
           
           
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.Activate();

            if (!Social.localUser.authenticated)
            {
                signIn();
            }


            MakeInstance();

        }
        else
        {

        }

    }
    public static void ShowLeaderBoardUI()
    {
      
       
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
    }
   
    #endregion
}
