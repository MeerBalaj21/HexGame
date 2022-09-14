using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayItems : MonoBehaviour
{
    public TMP_Text CoinText;
    public TMP_Text SkipText;
    public Skips Skip;
    public CoinSystem Coins;
    public int Coin;
    public int SkipCount;

    public void Start()
    {
        Coin = Coins.GetCoins();
        SkipCount = Skip.GetSkips();
       
        ShowItems();
    }
    public void ShowItems()
    {
        CoinText.SetText(Coin.ToString());
        SkipText.SetText(SkipCount.ToString());
    }
    public void Update()
    {
        if(Coin < Coins.GetCoins())
        {
            Coin = Coins.GetCoins();
            ShowItems();
        }
        if (SkipCount < Skip.GetSkips() || SkipCount > Skip.GetSkips())
        {
            SkipCount = Skip.GetSkips();
            ShowItems();
        }
    }
}
