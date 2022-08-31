using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Inputs _iS;
    [SerializeField] private Tray _tray;
    [SerializeField] private SearchDirection _sD;
    [SerializeField] private LevelGeneration _lG;

    private void Start()
    {
        _iS.Initialised(_tray);
        _tray.Initialised(_lG, _sD);
        _sD.Initialise(_lG);
    }

}
