using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour, IInputState
{
    private Vector2 _startTouchPos, _endTouchPos;
    private float _dist;
    private InputState _state;
    protected IInput _input;
    private Touch _touch;
    //private bool _isDraggable;
    public Camera _cam;

    private void Start()
    {
        _startTouchPos = new Vector2(2f, -4.75f);
    }

    public void Initialised(Tray Input)
    {
        _input = Input;
        ChangeState(new IdleInputState(this,Input));
    }
    public void ChangeState(InputState state)
    {
        _state = state;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {

                _dist = Vector2.Distance(_startTouchPos,_cam.ScreenToWorldPoint(_touch.position));
                if(_dist < 1.5f)
                {
                    _state.Begin(_touch);

                }
            }
            else if (_touch.phase == TouchPhase.Moved)
            {
                _state.Move(_touch);
            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                _state.End(_touch);
            }

        }
        
    }

}
