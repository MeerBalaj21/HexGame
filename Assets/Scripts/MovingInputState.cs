using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInputState : InputState
{
    public MovingInputState(IInputState listener, IInput input ) : base(listener,input)
    {

    }
    public override void Move(Touch touch)
    {
        Input.Drag(touch);
    }
    public override void End(Touch touch)
    {
        Input.Snap(touch);
        Listener.ChangeState(new IdleInputState(this.Listener,this.Input));
    }

    public override void Begin(Touch touch)
    {
        
    }
}
