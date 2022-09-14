using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/CoinSystemSO", fileName = "CoinSystem")]
public class CoinSystem : ScriptableObject
{

    public int Coins;
    
    public void SetCoins(int _coins)
    {
        Coins += _coins;
    }
    public int GetCoins()
    {
        return Coins;
    }

}
