using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUps : MonoBehaviour
{
    public Flags Flag;
    public GameObject PopUpPanel;
    public TMP_Text PopupText;
    public PopUpsSO PopUp;


    private void OnEnable()
    {
        PopUp.OnPopupEnabled += Initialize;
    }

    private void OnDisable()
    {
        PopUp.OnPopupEnabled -= Initialize;
    }

    public void Initialize(string text)
    {
        PopupText.text = text;
        EnablePopup();
    }
    public void DisablePopUp()
    {
        Flag.InputFlag = false;
        PopUpPanel.SetActive(false);
    }
    public void EnablePopup()
    {
        Flag.InputFlag = true;
        PopUpPanel.transform.gameObject.SetActive(true);
        Debug.LogError("enable");
    }

    
}
