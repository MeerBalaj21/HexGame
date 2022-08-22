using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInputState : InputState
{
    public MovingInputState(IInputState listener, IInput input ) : base(listener,input)
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
        Debug.Log("end of moving state is called");
        Listener.ChangeState(new IdleInputState(this.Listener,this.Input));
    }

    public override void Begin()
    {
        
    }
}
