using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Inputs _iS;
    [SerializeField] private Tray _tray;

    private void Start()
    {
        _iS.Initialised(_tray);
    }

}
