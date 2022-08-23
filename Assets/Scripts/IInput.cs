using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public void Tap(Touch touch);
    public void Drag(Touch touch);
    public void Drop(Touch touch);
    public void Snap(Touch touch);

}
