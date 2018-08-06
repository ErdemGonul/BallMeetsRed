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
                Debug.Log("loggednnnn");
            }
            else
            {
                Debug.Log("codulnt");
            }
        }
            );
    }
	// Update is called once per frame
	void Update () {
		
	}
    #region     LeaderBoards

    public static void AddScoreToLeaderBoard(string leaderboardID,int score)
    {
        Social.ReportScore(score, leaderboardID,success => { });
    }
    public void connectIt()
    {
        if ( (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork))
        {
           
            Debug.Log("bak bu giriş");
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.Activate();

            if (!Social.localUser.authenticated)
            {
                signIn();
            }


            MakeInstance();

        }

    }
    public static void ShowLeaderBoardUI()
    {
      
       
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
    }

    #endregion
}
