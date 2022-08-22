using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour, IInputState
{
    private Vector2 _screenPosition, _startTouchPos, _endTouchPos;
    private Vector3 _worldPosition;
    private InputState _state;
    private IInput _input;
    private Touch _touch;
    private bool _isDraggable;
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
    public void OnStart()
    {
        _state.Begin();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {

            _touch = Input.GetTouch(0);
            ////Debug.Log(Input.touchCount);
            if (_touch.phase == TouchPhase.Began)
            {
                //_startTouchPos = _touch.position;
                OnStart();
            }
            if (_touch.phase == TouchPhase.Moved)
            {

                _state.Move();
            }
            if (_touch.phase == TouchPhase.Ended)
            {
                _state.End();
            }




            //if(_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Ended)
            //{
            //    _endTouchPos = _touch.position;

            //    float x = _endTouchPos.x - _startTouchPos.x;
            //    float y = _endTouchPos.y - _startTouchPos.y;

            //    if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
            //    {
            //        //_input.Tap();
            //        _state.End();
            //    }
            //    else
            //    {
            //        _state.Move();
            //        //_input.Drag();
            //    }

            //}

            // _screenPosition = Input.GetTouch(0).position;
        }
        //else
        //{
        //    return;
        //}
        //_worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);


    }



}
