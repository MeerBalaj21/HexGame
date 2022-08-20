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
        throw new System.NotImplementedException();
    }

    public override void End()
    {
        //base.End();
        //Listener.TapDetected
    }

    public override void Move()
    {
        //base.Move();
        Listener.ChangeState(new MovingInputState(this.Listener));
    }
}
