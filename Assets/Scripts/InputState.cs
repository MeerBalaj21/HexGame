using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputState
{

    public IInputState Listener;

    public InputState(IInputState listener)
    {
        Listener = listener;
    }

    public abstract void Begin();
    public abstract void Move();
    public abstract void End();
 
}
