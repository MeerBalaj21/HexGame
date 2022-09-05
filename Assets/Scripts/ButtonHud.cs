using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ButtonHud : MonoBehaviour
{
    public HorizontalScrollSnap Pages;

    public void LeftTwoButton()
    {
        Pages.GoToScreen(0);
    }
    public void LeftOneButton()
    {
        Pages.GoToScreen(1);
    }
    public void HomeButton()
    {
        Pages.GoToScreen(2);
    }
    public void RightOneButton()
    {
        Pages.GoToScreen(3);
    }
    public void RightTwoButton()
    {
        Pages.GoToScreen(4);
    }
}
