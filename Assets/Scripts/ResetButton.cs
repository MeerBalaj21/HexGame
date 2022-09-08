using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour, IAds
{
    public InterstitialAds InterstitialAds;



    public void ResetB()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitB()
    {
        //AdPlayer.IAD();
        //AdPlayer.IAD();
        InterstitialAds.ShowAd(this);
    }

    public void AdShown()
    {
        
    }

    public void AdClosed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
