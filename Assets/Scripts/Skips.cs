using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkipsSO", fileName = "Skips")]
public class Skips : ScriptableObject
{
    public int SkipCount = 0;

    public void SetSkips(int _skip)
    {
        SkipCount += _skip;
    }

    public void SetPrefSkips(int _skip)
    {
        SkipCount = _skip;
    }

    public int GetSkips()
    {
        return SkipCount;
    }
    public void DecreaseSkip()
    {
        if(SkipCount != 0)
        {
            SkipCount = SkipCount -1;
        }
    }
}
