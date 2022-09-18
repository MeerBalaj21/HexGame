using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/RewardHandlerSO", fileName = "RewardHandler")]
public class RewardHandler : ScriptableObject
{
    public IAPStore Store;
    public CoinSystem Coins;
    public Skips Skip;
    public NoAds RemoveAds;

    public void Initialise()
    {
        var Ads = PlayerPrefs.GetInt("RemoveAds");
        Debug.LogError(Ads);
        if (Ads == 1)
        {
            RemoveAds.ShowAds = false;
        }

        Coins.SetPrefCoins(PlayerPrefs.GetInt("Coins"));
        Skip.SetPrefSkips(PlayerPrefs.GetInt("Skips"));
    }


    public void GiveReward(IAPItems items)
    {
        foreach (var i in items.RewardType)
        {
            if (i.Rewards == RewardType.Coin)
            {
                Coins.SetCoins(i.Amount);
                PlayerPrefs.SetInt("Coins", i.Amount);
                PlayerPrefs.Save();
            }
            if (i.Rewards == RewardType.Skip)
            {
                Skip.SetSkips(i.Amount);
                PlayerPrefs.SetInt("Skips", i.Amount);
                PlayerPrefs.Save();
            }
            if (i.Rewards == RewardType.RemoveAds)
            {
                if(RemoveAds.ShowAds == true)
                {
                    RemoveAds.DontAllowAds();
                    PlayerPrefs.SetInt("RemoveAds", 1);
                    PlayerPrefs.Save();
                }
            }
        }
        
    }
}
