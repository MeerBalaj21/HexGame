using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputState
{

    public IInputState Listener;
    public IInput Input;
    public InputState(IInputState listener, IInput input)
    {
        Listener = listener;
        Input = input;
    }

    public abstract void Begin();
    public abstract void Move();
    public abstract void End();
 
}
