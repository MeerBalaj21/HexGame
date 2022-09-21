using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallibrationInputState : InputState
{
    public CallibrationInputState(IInputState listener, IInput input) : base(listener, input)
    {

    }

    public override void Begin(Touch touch)
    {
        
    }

    public override void End(Touch touch)
    {
        if (Mathf.Abs(touch.deltaPosition.x) < 1.5f && Mathf.Abs(touch.deltaPosition.y) < 1.5f)
        {
            Input.Tap(touch);
            Debug.Log("tapped");
            Listener.ChangeState(new IdleInputState(this.Listener, this.Input));
        }
    }

    public override void Move(Touch touch)
    {
        Debug.LogError(touch.deltaPosition);
        if (Mathf.Abs(touch.deltaPosition.x) >= 1.5f && Mathf.Abs(touch.deltaPosition.y) >= 1.5f)
            Listener.ChangeState(new MovingInputState(this.Listener, this.Input));
    }
}
