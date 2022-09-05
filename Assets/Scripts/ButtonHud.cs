using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHud : MonoBehaviour
{
    public Scrollbar Pages;

    public void LeftTwoButton()
    {
        Pages.value = 0.0f;
    }
    public void LeftOneButton()
    {
        Pages.value = 0.25f;
    }
    public void HomeButton()
    {
        Pages.value = 0.5f;
    }
    public void RightOneButton()
    {
        Pages.value = 0.75f;
    }
    public void RightTwoButton()
    {
        Pages.value = 1.0f;
    }
}
