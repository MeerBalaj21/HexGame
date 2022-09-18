using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/PopUpsSO", fileName = "PopUps")]

public class PopUpsSO : ScriptableObject
{
    public Action<string> OnPopupEnabled;

    public void EnablePopUp(string text)
    {
        OnPopupEnabled?.Invoke(text);
    }
}
