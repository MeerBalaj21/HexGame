using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetQuitFail : MonoBehaviour, IAds
{
    public InterstitialAds InterstitialAds;
    public GameObject QuitPopUp;
    public GameObject ResetPopUp;
    public GameObject LevelFail;
    public GameObject ParentPanel;
    public FailCondition Fail;
    public bool ResetCondition;
    public Flags Flag;

    public void Start()
    {
        QuitPopUp.SetActive(false);
        LevelFail.SetActive(false);
        ResetPopUp.SetActive(false);
        ParentPanel.SetActive(false);
    }

    public void ResetB()
    {
        Flag.InputFlag = true;
        ParentPanel.SetActive(true);
        ResetPopUp.SetActive(true);
    }


    public void ResetCancel()
    {
        Flag.InputFlag = false;
        ResetPopUp.SetActive(false);
        ParentPanel.SetActive(false);
    }

    public void Reset()
    {
        if(Fail.Fail == true)
        {
            ResetCondition = true;
            InterstitialAds.ShowAd(this);
        }
        else
        {
            Flag.InputFlag = false;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void QuitB()
    {
        InterstitialAds.ShowAd(this);
        //Flag.InputFlag = false;
        QuitPopUp.SetActive(false);
        ParentPanel.SetActive(false);
    }

    public void Quit()
    {
        Flag.InputFlag = true;
        ParentPanel.SetActive(true);
        QuitPopUp.SetActive(true);
    }

    public void QuitCancel()
    {
        Flag.InputFlag = false;
        QuitPopUp.SetActive(false);
        ParentPanel.SetActive(false);
    }

    public void Update()
    {
        if(Fail.Fail == true)
        {
            Flag.InputFlag = true;
            ParentPanel.SetActive(true);
            LevelFail.SetActive(true);
        }
    }

    public void AdShown()
    {
        
    }

    public void AdClosed()
    {
        if (ResetCondition == true)
        {
            ResetCondition = false;
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        Flag.InputFlag = false;
    }

    
}
