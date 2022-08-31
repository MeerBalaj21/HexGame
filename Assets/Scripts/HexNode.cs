using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour
{
    public List<Sprite> _sprites;
    public Vector2 _locations;
    private SpriteRenderer _sP;
    private int _index;
    [SerializeField] private Vector2 _xy;
    [SerializeField] public int Value;
    public bool IsGrab = false;
    public bool RowFive = true;


    public void SetXY(Vector2 xy)
    {
        _xy = xy;
    }
    public void SetValue(int v)
    {
        Value = v;
    }
    public void SetLocations(Vector2 loc)
    {
        _locations = loc;
    }
    public void SetGrab()
    {
        IsGrab = true;
    }
    public void ResetGrab()
    {
        IsGrab = false;
    }
    public void SpriteChanger(int index)
    {
        //if (index > Value)
        
            _sP = gameObject.GetComponent<SpriteRenderer>();
            _sP.sprite = _sprites[index];
        
        //else
        //{
            
        //}
        
    }
    public void SortLayerOrder()
    {
        _sP.sortingOrder = 2;
    }
    public void ResetLayerOrder()
    {
        _sP.sortingOrder = 1;
    }
    public void SetIndex(int id)
    {
        _index = id;
    }
    public int GetIndex()
    {
        return _index;
    }
}
