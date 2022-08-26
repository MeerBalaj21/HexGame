using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, IInput
{
    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 _defaultPos = new Vector2(2f,-4.75f);
    private Vector2 _BaseTrayPos;
    private Vector3 _lastPos;
    private Vector3 _CheckCondtion;
    private GameObject _child;
    private GameObject _childTwo;
    private Vector2 _childPos, _childTwoPos;
    public LevelGeneration LG;
    [SerializeField] private GameObject _hex;
    [SerializeField] private Camera _cam;
    
    //private LevelGeneration _lG;
    //[SerializeField] private float _timer = 0;


    private void Start()
    {
        _CheckCondtion = new Vector3(-1, -1, -1);
        _BaseTrayPos = new Vector2(2, -4.75f);
        Initialise();

    }

    public void Initialise()
    {
        //_defaultPos = transform.localPosition;
        _child = transform.GetChild(0).gameObject;
        _childPos = _child.transform.localPosition;
        if (transform.childCount > 1)
        {
            _childTwo = transform.GetChild(1).gameObject;
            _childTwoPos = transform.localPosition;
        }
    }

    public void Initialised(LevelGeneration _LG)
    {
        LG = _LG;
    }

    public void Tap(Touch touch)
    {
        Initialise();
        transform.Rotate(0f, 0f, 60f);
        _child.transform.Rotate(0f, 0f, -60f);
        _childTwo.transform.Rotate(0f, 0f, -60f);
    }

    public void Drag(Touch touch)
    {
        //Initialise();
        if(transform.childCount == 1)
        {
            Vector2 offset = _cam.ScreenToWorldPoint(touch.position);
            offset.y = offset.y + 1;
            LG.GridHighLight(offset, Vector2.zero);
        }
        else if( transform.childCount != 1)
        {
            Vector2 delta = _childTwo.transform.position - _child.transform.position;
            LG.GridHighLight(_child.transform.position, delta);
        }

        var endPoint = _cam.ScreenToWorldPoint(touch.position);
        var dist = Vector2.Distance(_startPos, endPoint);
        _startPos = transform.position;
        _endPos = endPoint;
        _endPos.y = _endPos.y+1;
        transform.position = Vector2.Lerp(_startPos, _endPos, Time.deltaTime * 2 + dist);       
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
        
        //Debug.Log(transform.childCount);
        if (transform.childCount == 1)
        {
            Vector2 offset = _cam.ScreenToWorldPoint(touch.position);
            offset.y = offset.y + 1;
            _lastPos = (Vector3)LG.GridFind(offset, Vector2.zero);
            //Debug.Log(_lastPos);
            if (_lastPos != _CheckCondtion)
            {
                Initialise();
                transform.position = _lastPos;
                _child = transform.GetChild(0).gameObject;
                _child.transform.SetParent(LG.transform);

                transform.position = _BaseTrayPos;
                GameObject Hex = (GameObject)Instantiate(_hex, _defaultPos, Quaternion.identity);
                Hex.transform.SetParent(this.transform);
                //Hex.transform.position = Vector2.zero;
            }
            else
            {
                Drop(touch);
            }
        }
        else
        {
            Debug.Log("Snap two per ata hai");
            SnapTwo(touch);
        }

    }

    public void SnapTwo(Touch touch)
    {

        Initialise();
        Vector2 delta = _childTwo.transform.position - _child.transform.position;
        Debug.Log("Child 1 " + _child.transform.position);
        Debug.Log("Child 2 " + _childTwo.transform.position);

        Debug.Log("delta" + delta);
        _lastPos = (Vector3)LG.GridFind(_child.transform.position, delta);
        Debug.Log(_lastPos + "after returning from nearest");
        if(_lastPos != _CheckCondtion)
        {
            //Initialise();
            _child.transform.position = _lastPos - (Vector3)delta;
            _childTwo.transform.position = _lastPos;

            _child.transform.SetParent(LG.transform);
            _childTwo.transform.SetParent(LG.transform);
            transform.position = _BaseTrayPos;

            GameObject Hex = (GameObject)Instantiate(_hex, _childPos, Quaternion.identity);
            GameObject HexTwo = (GameObject)Instantiate(_hex, _childTwoPos, Quaternion.identity);

            Hex.transform.SetParent(this.transform);
            HexTwo.transform.SetParent(this.transform);

            Hex.transform.localPosition = new Vector2(-0.5f, 0f);
            HexTwo.transform.localPosition = new Vector2(0.5f, 0f);

        }
        else
        {
            Debug.Log("Else of the snap two function");
            Drop(touch);
        }


    }

    public void HexSpawner()
    {

    }

}
