using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour
{
    public List<Sprite> _sprites;
    private SpriteRenderer SP;

    [SerializeField] private Vector2 _xy;
    public Vector2 _locations;
    [SerializeField] private int _value;
    [SerializeField] private bool _isGrab;


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
    private void SpriteChanger()
    {
        SP = gameObject.GetComponent<SpriteRenderer>();
        SP.sprite = _sprites[_value];
    }
}
