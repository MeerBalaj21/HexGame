using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleInputState : InputState
{

    public IdleInputState(IInputState listener, IInput input) : base(listener, input)
    {

    }
    override public void Begin(Touch touch)
    {
        
        Listener.ChangeState(new CallibrationInputState(this.Listener,this.Input));
    }

    public override void End(Touch touch)
    {
        
    }

    public override void Move(Touch touch)
    {
        
    }
}
