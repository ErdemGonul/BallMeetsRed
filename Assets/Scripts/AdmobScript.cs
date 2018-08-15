using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdmobScript : MonoBehaviour
{
     RewardBasedVideoAd rewardBasedVideo;

    public static AdmobScript instance;

    InterstitialAd interstitial;
    // Use this for initialization
    private void Awake()
    {
        if (instance != null)
        {
            RequestInterstitial();
            RequestRewardBasedVideo();
        }
        else
        {
#if UNITY_ANDROID
            string appId = "ca-app-pub-3004677166037107~8018378409";
#endif
    PlayerPrefs.SetInt("adTime", 1);
                
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(appId);

            RequestInterstitial();
            RequestRewardBasedVideo();
            MakeInstance();

        }
    }
    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3004677166037107/4175453854";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }


    public void showRewarded()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin") + 100);
            
        }
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
    /*
    public static BannerView RequestBanner()
	{

        if (PlayerPrefs.GetInt("canAd") == 1)
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3004677166037107/7220302051";
#endif

            AdSize size = new AdSize(Screen.width, 50);
            // Create a 320x50 banner at the bottom of the screen.
            BannerView bannerView = new BannerView(adUnitId, size, AdPosition.Top);
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            bannerView.LoadAd(request);
            
            return bannerView;
        }
        else
            return null;
	}
    */
	public void adGive()
    {
        if (PlayerPrefs.GetInt("canAd") == 1)
        {
            if (interstitial.IsLoaded())
            {
                Debug.Log("göstermliyidim");
                interstitial.Show();
            }
            else { Debug.Log("hay"); }
        }
    }
	public InterstitialAd RequestInterstitial()
	{
        if (PlayerPrefs.GetInt("canAd")==1)
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3004677166037107/6408007169";
#endif
            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.

            AdRequest request = new AdRequest.Builder().Build();


            // Load the interstitial with the request.
            interstitial.LoadAd(request);

            return interstitial;
        }
        return interstitial;


    }
  

}