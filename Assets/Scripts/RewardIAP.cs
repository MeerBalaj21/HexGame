using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardIAP
{

    public Sprite Icon;
    public int Amount;
    public RewardType Rewards;

}
public enum RewardType
{
    RemoveAds,
    Coin, Skip
};