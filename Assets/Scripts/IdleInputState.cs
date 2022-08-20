using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleInputState : InputState
{

    public IdleInputState(IInputState listener) : base(listener)
    {

    }
    override public void Begin()
    {
        Listener.ChangeState(new CallibrationInputState(this.Listener));
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
}
