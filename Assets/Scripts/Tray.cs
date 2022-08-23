using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, IInput
{
    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 _defaultPos;
    private Vector3 _lastPos;
    [SerializeField] private Camera _cam;
    //[SerializeField] private float _timer = 0;
    private LevelGeneration _lG;


    private void Start()
    {
        _defaultPos = transform.localPosition;
    }
    public void Initialised(LevelGeneration LG)
    {
        _lG = LG;
    }
    public void Tap(Touch touch)
    {
       transform.Rotate(0f, 0f, 60f);
       var _child = transform.GetChild(0);
       _child.transform.Rotate(0f, 0f, -60f);
    }
    public void Drag(Touch touch)
    {
        //var _child = transform.GetChild(0);
        var endPoint = _cam.ScreenToWorldPoint(touch.position);
        var dist = Vector2.Distance(_startPos, endPoint);
        _startPos = transform.position;
        _endPos = endPoint;
        _endPos.y = _endPos.y+1;
        transform.position = Vector2.Lerp(_startPos, _endPos, Time.deltaTime *2 + dist );
        
    }
    public void Drop(Touch touch)
    {
        var dist = Vector2.Distance(_startPos, _defaultPos);
        _startPos = transform.position;
        _endPos = _defaultPos;
        transform.position = Vector2.Lerp(_startPos, _endPos, Time.deltaTime * 2 + dist);

    }

    public void Snap(Touch touch)
    {
        Vector2 offset = _cam.ScreenToWorldPoint(touch.position);
        offset.y = offset.y + 1;
        _lastPos = (Vector3)_lG.GridFind(offset);
        Debug.Log(_lastPos);
        if(_lastPos != new Vector3(-1,-1,-1))
        {
            transform.position = _lastPos;
        }
        else
        {
            Drop(touch);

        }
        
    }
}
