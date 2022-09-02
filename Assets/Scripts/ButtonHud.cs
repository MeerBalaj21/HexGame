using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHud : MonoBehaviour
{
    public Scrollbar Pages;

    public void HomeButton()
    {
        Pages.value = 0.5f;
    }
    public void LeftOneButton()
    {
        Pages.value = 0.3f;
    }
    public void LeftTwoButton()
    {
        Pages.value = 0.1f;
    }
    public void RightOneButton()
    {
        Pages.value = 0.7f;
    }
    public void RightTwoButton()
    {
        Pages.value = 0.9f;
    }
}
