using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RVCard : MonoBehaviour, IAds
{
    public CoinSystem Coin;
    public RewardedAd RVAd;
    public TMP_Text CoinText;
    public PopUpsSO PopUp;

    [SerializeField] private int _coins = 100;

    public void AddCoins()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            PopUp.EnablePopUp("Ad not available");
        }
        else
        {
            RVAd.ShowAd(this);
        }
    }

    public void AdClosed()
    {
        Coin.SetCoins(_coins);
        CoinText.SetText("+" +_coins.ToString() + " Coins");
        PlayerPrefs.SetInt("Coins", Coin.GetCoins());

        PopUp.EnablePopUp("Purchase succesful");
    }

    public void AdShown()
    {
        
    }
}
