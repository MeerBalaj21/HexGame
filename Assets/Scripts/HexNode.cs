using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour
{
    [SerializeField] private Vector2 _xy;
    public Vector2 _locations;
    public List<Sprite> _sprites;
    public int Value;
    public bool IsGrab = false;
    public bool RowFive;
    private SpriteRenderer _sP;

    public void SetXY(Vector2 xy)
    {
        _xy = xy;
    }
    public Vector2 GetXY()
    {
        return _xy;
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
        _sP = gameObject.GetComponent<SpriteRenderer>();
        _sP.sprite = _sprites[index];
    }
    public void SortLayerOrder()
    {
        _sP.sortingOrder = 2;
    }
    public void ResetLayerOrder()
    {
        _sP.sortingOrder = 1;
    }
}
