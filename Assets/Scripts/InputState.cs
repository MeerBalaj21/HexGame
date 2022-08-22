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

    public abstract void Begin(Touch touch);
    public abstract void Move(Touch touch);
    public abstract void End(Touch touch);
 
}
