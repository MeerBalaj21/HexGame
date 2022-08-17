using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour
{
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private int _value;
    public List<Sprite> _sprites;
    private SpriteRenderer SP;

    public void SetX(int x)
    {
        _x = x;
    }
    public void SetY(int y)
    {
        _y = y;
    }
    public void SetValue(int v)
    {
        _value = v;
    }

    private void SpriteChanger()
    {
        SP = gameObject.GetComponent<SpriteRenderer>();
        SP.sprite = _sprites[_value];
    }
}
