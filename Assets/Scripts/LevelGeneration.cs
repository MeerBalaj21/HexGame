using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;

    public GameObject Hex;
    private HexNode _hex;

    [SerializeField] private Dictionary<string, HexNode> HexDic = new Dictionary<string, HexNode>();

    private void Start()
    {
        GridSetup();
    }

    public Vector3 GridFind(Vector2 pos)
    {
        foreach(var pair in HexDic)
        {
            float distance = Vector2.Distance(pos, pair.Value._locations);
            if (distance < 0.5f)
            {
                Vector2 loc = pair.Value._locations;
                return loc;
            }
            
        }
        return new Vector3(-1, -1, -1); ;
    }
    private void GridSetup()
    {
        for (int x = 0; x < _rows; x++)
        {
            for (int y = 0; y < _columns; y++)
            {
                float xPos = x * _xOffset;

                if (y % 2 == 1)
                {
                    xPos = xPos + (_xOffset / 2);
                }
                if ((y % 2 == 1) && (x == _rows - 1))
                {
                    continue;
                }

                GameObject HexGo = (GameObject)Instantiate(Hex, new Vector2(xPos, y * _yOffset), Quaternion.identity);
                string name = "Hex_" + x + ", " + y;
                HexGo.name = name;
                _hex = HexGo.GetComponent<HexNode>();
                _hex.SetXY(new Vector2(x,y));
                _hex.SetLocations(new Vector2(xPos, y * _yOffset));
                _hex.SetValue(0);
                HexDic.Add(name, _hex);
                HexGo.transform.SetParent(this.transform);

            }
        }
    }
}
