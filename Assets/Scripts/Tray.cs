using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, IInput
{
    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 _defaultPos;

    [SerializeField] private Camera _cam;
    [SerializeField] private float _timer = 0;

    private void Start()
    {
        _defaultPos = transform.localPosition;
    }

    public void Tap(Touch touch)
    {
        //transform.rotation = Quaternion.Lerp(_startRot, _endRot, _timer);
       transform.Rotate(0f, 0f, 60f);
    }
    public void Drag(Touch touch)
    {
        var endPoint = _cam.ScreenToWorldPoint(touch.position);
        var dist = Vector2.Distance(_startPos, endPoint);
        _startPos = transform.position;
        _endPos = endPoint;

        //Debug.LogError(_endPos);
        //Debug.LogError(_startPos);
        transform.position = Vector2.Lerp(_startPos, _endPos, Time.deltaTime *2 + dist );
        
    }
    public void Drop(Touch touch)
    {
        var dist = Vector2.Distance(_startPos, _defaultPos);
        _startPos = transform.position;
        _endPos = _defaultPos;

        //Debug.LogError(_endPos);
        //Debug.LogError(_startPos);
        transform.position = Vector2.Lerp(_startPos, _endPos, Time.deltaTime * 2 + dist);

    }
}
