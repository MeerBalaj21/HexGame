using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/RewardedSO", order = 1, fileName = "RewardedAd")]
public class RewardedAd : ScriptableObject
{
    IAds _interAdListener;
    public Skips Skip;
    string adUnitId = "585f249ad115c420";
    int retryAttempt;
    long timer = 0;
    long lastAdTime = 0;
    public InterstitialAds InterstitialAd;
    //public Tray Tray;
    //public GameObject Skip;


    public void InitializeRewardedAds()
    {
        MaxSdk.LoadRewardedAd(adUnitId);
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }
    public string GetID()
    {
        return adUnitId;
    }
    public long GetTimer()
    {
        timer = DateTimeOffset.Now.ToUnixTimeSeconds();
        return timer;
    }
    public long GetLastTimer()
    {
        return lastAdTime;
    }
    //public void SetTimer(long time)
    //{
        
    //}
    public void ShowAd(IAds AdListener)
    {
        _interAdListener = AdListener;

        LoadRewardedAd();

        if (MaxSdk.IsRewardedAdReady(adUnitId))
        {
            if(Skip.GetSkips() == 0)
            {
                MaxSdk.ShowRewardedAd(adUnitId);
            }
            else
            {
                AdListener.AdClosed();
            }
            
        }
        else
        {
            LoadRewardedAd();
            AdListener.AdClosed();
        }
    }
    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(adUnitId);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.
        //Debug.LogError("OnRewardedAdLoadedEvent");
        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).
        //Debug.LogError("OnRewardedAdLoadFailedEvent");
        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        //Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError("OnRewardedAdDisplayedEvent");

    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        //Debug.LogError("OnRewardedAdFailedToDisplayEvent");
        LoadRewardedAd();
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError("OnRewardedAdClickedEvent");
    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        //Debug.LogError("OnRewardedAdHiddenEvent");
        InterstitialAd.SetLastAdTimer(GetTimer());
        LoadRewardedAd();
      
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        //Debug.LogError("OnRewardedAdReceivedRewardEvent");
        _interAdListener.AdClosed();
        //Tray.SkipTile();
        
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError("OnRewardedAdRevenuePaidEvent");
        // Ad revenue paid. Use this callback to track user revenue.
    }
}
