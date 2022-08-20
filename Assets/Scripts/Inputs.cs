using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour, IInputState
{
    private InputState _state;
    private IInput _input;

    private bool _isDraggable;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    //private Draggable _lastDragged;

    public void Initialised(Tray Input)
    {
        _input = Input;
        ChangeState(new IdleInputState(this));
    }
    public void ChangeState(InputState state)
    {
        _state = state;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }
        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        
    }



}
