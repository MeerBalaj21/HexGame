using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInputState : InputState
{
    public MovingInputState(IInputState listener) : base(listener)
    {

    }
    public override void Move()
    {
        //base.Move();
        //Drag Move
    }
    public override void End()
    {
        //base.End();
        //Drag End
    }

    public override void Begin()
    {
        throw new System.NotImplementedException();
    }
}
