using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour, IAds
{
    public InterstitialAds InterstitialAds;
    public GameObject QuitPopUp;
    public GameObject ResetPopUp;
    public GameObject ParentPanel;

    public void Start()
    {
        QuitPopUp.SetActive(false);
        ParentPanel.SetActive(false);
    }

    public void ResetB()
    {
        ParentPanel.SetActive(true);
        ResetPopUp.SetActive(true);
    }


    public void ResetCancel()
    {
        ResetPopUp.SetActive(false);
        ParentPanel.SetActive(false);
    }

    public void Reset()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitB()
    {
        //AdPlayer.IAD();
        //AdPlayer.IAD();
        InterstitialAds.ShowAd(this);
    }

    public void Quit()
    {
        ParentPanel.SetActive(true);
        QuitPopUp.SetActive(true);
    }

    public void QuitCancel()
    {
        QuitPopUp.SetActive(false);
        ParentPanel.SetActive(false);
    }

    public void AdShown()
    {
        
    }

    public void AdClosed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
