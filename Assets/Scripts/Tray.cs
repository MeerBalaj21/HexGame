using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, IInput
{
    private Quaternion _startRot;
    private Quaternion _endRot;
    [SerializeField] private float _timer = 0;

    public void Tap()
    {
        //transform.rotation = Quaternion.Lerp(_startRot, _endRot, _timer);
       transform.Rotate(0f, 0f, 60f);
    }
    public void Drag()
    {

    }
    public void Drop()
    {

    }
}
