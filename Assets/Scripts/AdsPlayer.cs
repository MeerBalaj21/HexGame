using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsPlayer : MonoBehaviour, IAds
{
    public PopUpsSO PopUp;
    private const string MaxKey = "hlKffQFn1sKXRefAUUKG4o-i-OOURETonfImCKvE29oyDwftIiyhVZMlNNxwUFl8NgUmynX33XOEq5m09yb34Z";
    private const string RewardedAdUnit = "585f249ad115c420";
    private const string InterstitialAdUnit = "7d62e5180461f57a";
    private const string BannerAdUnit = "b56d58800dadb2d1";

    public InterstitialAds InterstitialAd;
    public RewardedAd RewardedAds;
    //IInterstitlalAd listener;

    private bool _interAdHidden;
    
    
    private void Start()
    {
        string[] adUnitIds = {
            // rewarded
            RewardedAdUnit,
            // interstitial
            InterstitialAdUnit,
            // banner
            BannerAdUnit
        };

        MaxSdk.SetSdkKey(MaxKey);
        MaxSdk.SetUserId(SystemInfo.deviceUniqueIdentifier);
        MaxSdk.SetVerboseLogging(true);
        //MaxSdk.LoadRewardedAd(RewardedAdUnit);
        MaxSdkCallbacks.OnSdkInitializedEvent += OnMaxInitialized;
        MaxSdk.InitializeSdk(adUnitIds);
    }
    private void OnMaxInitialized(MaxSdkBase.SdkConfiguration sdkConfiguration)
    {
        if (MaxSdk.IsInitialized())
        {
            #if DEVELOPMENT_BUILD || UNITY_EDITOR
                MaxSdk.ShowMediationDebugger();
            #endif
            Debug.Log("MaxSDK initialized");

            InterstitialAd.InitializeInterstitialAds();
            RewardedAds.InitializeRewardedAds();
        }
        else
        {
            Debug.Log("Failed to init MaxSDK");
        }


    }

    public void IAD()
    {
        if(!MaxSdk.IsInitialized())
        {
            PopUp.EnablePopUp("No Ads Available");
        }
        else
        {
            MaxSdk.ShowInterstitial(InterstitialAdUnit);
        }
    }
    public void RAD()
    {
        if (!MaxSdk.IsInitialized())
        {
            PopUp.EnablePopUp("No Ads Available");
        }
        else
        {
            MaxSdk.ShowRewardedAd(RewardedAdUnit);
        }
        
    }

    public IEnumerator ShowInterstitialAd()
    {
        _interAdHidden = false;
        InterstitialAd.ShowAd(this);

        yield return new WaitUntil(() => _interAdHidden);
    }

    public void AdShown()
    {
        _interAdHidden = false;
    }

    public void AdClosed()
    {
        _interAdHidden = true;
    }

    public void MaxDebug()
    {
        MaxSdk.ShowMediationDebugger();
    }
}