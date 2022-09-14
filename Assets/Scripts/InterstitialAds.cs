using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/InterstitialSO", order = 1, fileName = "InterstitialAd")]
public class InterstitialAds : ScriptableObject
{
    IAds _interAdListener;
    string adUnitId = "7d62e5180461f57a";
    public NoAds Ads;
    public RewardedAd RewardedAds;
    int retryAttempt;
    long timer = 0;
    long LastAdTimer = 0;

    public long GetTimer()
    {
        timer = DateTimeOffset.Now.ToUnixTimeSeconds();
        return timer;
    }

    public void InitializeInterstitialAds()
    {
        LastAdTimer = 0;
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    public void ShowAd(IAds interAdListener)
    {
        _interAdListener = interAdListener;

        if (MaxSdk.IsInterstitialReady(adUnitId))
        {
            if (GetTimer() - LastAdTimer > 30 && Ads.ShowAds)
            {
                MaxSdk.ShowInterstitial(adUnitId);

            }
            else
            {
                interAdListener.AdClosed();
            }
        }
        else
        {
            LoadInterstitial();
            interAdListener.AdClosed();
        }

    }
    public void SetLastAdTimer(long time)
    {
        LastAdTimer = time;
    }

    private void LoadInterstitial()
    {
        //Debug.LogError("LoadInterstitial");
        MaxSdk.LoadInterstitial(adUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'
        //Debug.LogError("OnInterstitialLoadedEvent");
        // Reset retry attempt
        retryAttempt = 0;
        //_interAdListener.AdShown();
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)
        //Debug.LogError("OnInterstitialLoadFailedEvent");
        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
       
        //Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError("OnInterstitialDisplayedEvent");
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError("OnInterstitialAdFailedToDisplayEvent");
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError("OnInterstitialClickedEvent");
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.LogError("OnInterstitialHiddenEvent");
        // Interstitial ad is hidden. Pre-load the next ad.
        LastAdTimer = timer;
        LoadInterstitial();
        _interAdListener.AdClosed();
    }

}
