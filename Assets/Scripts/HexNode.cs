using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour
{
    public List<Sprite> _sprites;
    public Vector2 _locations;
    private SpriteRenderer _sP;

    [SerializeField] private Vector2 _xy;
    [SerializeField] private int _value;
    public bool _isGrab = false;


    public void SetXY(Vector2 xy)
    {
        _xy = xy;
    }
    public void SetValue(int v)
    {
        _value = v;
    }
    public void SetLocations(Vector2 loc)
    {
        _locations = loc;
    }
    public void SetGrab()
    {
        _isGrab = true;
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
