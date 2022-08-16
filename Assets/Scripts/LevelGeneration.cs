using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject Hex;

    [SerializeField] private int _rows;
    [SerializeField] private int _columns;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;


    private void Start()
    {
        for(int x = 0; x < _rows; x++)
        {
            for(int y = 0; y < _columns; y++)
            {
                float xPos = x * _xOffset;

                if( y % 2 == 1)
                {
                    xPos = xPos + (_xOffset / 2);

                }
                if ((y % 2 == 1) && (x == _rows - 1))
                {
                    continue;
                }

                GameObject HexGo = (GameObject)Instantiate(Hex, new Vector2(xPos, y * _yOffset), Quaternion.identity);

                HexGo.name = "Hex_" + x + ", " + y;
                HexGo.transform.SetParent(this.transform);

            }
        }
    }
}
