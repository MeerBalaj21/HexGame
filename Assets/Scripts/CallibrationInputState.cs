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
        Input.Tap(touch);
        Debug.Log("tapped");
        Listener.ChangeState(new IdleInputState(this.Listener, this.Input));
    }

    public override void Move(Touch touch)
    {
        Listener.ChangeState(new MovingInputState(this.Listener, this.Input));
    }
}
