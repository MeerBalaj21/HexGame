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

    public void GiveReward(IAPItems items)
    {
        foreach (var i in items.RewardType)
        {
            if (i.Rewards == RewardType.Coin)
            {
                Coins.SetCoins(i.Amount);
            }
            if (i.Rewards == RewardType.Skip)
            {
                Skip.SetSkips(i.Amount);
            }
            if (i.Rewards == RewardType.RemoveAds)
            {
                if(RemoveAds.ShowAds == true)
                {
                    RemoveAds.DontAllowAds();
                }
            }
        }
    }
}
