using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/NoAdsSo", fileName = "NoAds")]
public class NoAds : ScriptableObject
{
    public bool ShowAds;

    public void AllowAds()
    {
        ShowAds = true;
    }
    public void DontAllowAds()
    {
        ShowAds = false;
    }
}
