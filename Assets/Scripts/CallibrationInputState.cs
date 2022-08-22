using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallibrationInputState : InputState
{
    public CallibrationInputState(IInputState listener) : base(listener)
    {

    }

    public override void Begin()
    {
        
    }

    public override void End()
    {
        //base.End();
        //Listener.TapDetected
        Debug.Log("tapped");
        Listener.ChangeState(new IdleInputState(this.Listener));
    }

    public override void Move()
    {
        //base.Move();
        Listener.ChangeState(new MovingInputState(this.Listener));
    }
}
