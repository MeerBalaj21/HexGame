using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour
{
    public List<Sprite> _sprites;
    public Vector2 _locations;
    private SpriteRenderer SP;

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
        SP = gameObject.GetComponent<SpriteRenderer>();
        SP.sprite = _sprites[index];
    }
}
