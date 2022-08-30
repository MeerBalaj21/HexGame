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

    public Vector3 GridFind(Vector2 pos, Vector2 delta)
    {
        
        if(delta == Vector2.zero)
        {
            foreach (var pair in HexDic)
            {
                float distance = Vector2.Distance(pos, pair.Value._locations);
                if (distance <= 0.5f && (pair.Value._isGrab == false))
                {
                    Vector2 loc = pair.Value._locations;
                    pair.Value.SetGrab();
                    Debug.Log("delta zero");
                    pair.Value.transform.GetChild(0).gameObject.SetActive(false);
                    return loc;
                }
            }
        }
        else if(delta != Vector2.zero)
        {
            foreach (var pair in HexDic)
            {
                float distance = Vector2.Distance(pos, pair.Value._locations);
                if (distance <= 0.5f && (pair.Value._isGrab == false))
                {
                    Vector2 loc = pair.Value._locations;
                    
                    loc = loc + delta;
                   
                    foreach( var secondPair in HexDic)
                    {
                        if ((loc - secondPair.Value._locations).magnitude < 0.49 && secondPair.Value._isGrab == false)
                        {
                            Debug.Log("found" + loc);
                            pair.Value.SetGrab();
                            secondPair.Value.SetGrab();

                            pair.Value.transform.GetChild(0).gameObject.SetActive(false);
                            secondPair.Value.transform.GetChild(0).gameObject.SetActive(false);
                            //secondPair.Value.SpriteChanger(0);
                            return loc;
                        }
                    }
                }
            }
        }
        return new Vector3(-1, -1, -1);        
    }


    public void GridHighLight(Vector2 pos, Vector2 delta)
    {
        if(delta == Vector2.zero)
        {
            foreach (var pair in HexDic)
            {
                float distance = Vector2.Distance(pos, pair.Value._locations);
                if (distance <= 0.5f && (pair.Value._isGrab == false))
                {
                    pair.Value.transform.GetChild(0).gameObject.SetActive(true);
                    //pair.Value.GetComponent<SpriteRenderer>().color = Color.grey;
                    //pair.Value.SpriteChanger(1);
                }
                else if (distance > 0.5f)
                {
                    pair.Value.transform.GetChild(0).gameObject.SetActive(false);
                    //pair.Value.GetComponent<SpriteRenderer>().color = Color.white;
                    //pair.Value.SpriteChanger(0);
                }
            }
        }
        else if(delta != Vector2.zero)
        {
            HexNode first = null;
            HexNode second = null;

            foreach (var pair in HexDic)
            {
                float distance = Vector2.Distance(pos, pair.Value._locations);
                if (distance <= 0.5f && (pair.Value._isGrab == false))
                {
                    first = pair.Value;

                    Vector2 loc = pair.Value._locations;
                    loc = loc + delta;
                    foreach (var secondPair in HexDic)
                    {
                        if (first != secondPair.Value)
                        {
                            if ((loc - secondPair.Value._locations).magnitude < 0.49 && secondPair.Value._isGrab == false)
                            {
                                second = secondPair.Value;
                                break;
                            }
                        }
                    }

                    break;
                }
            }

            foreach(var pair in HexDic)
            {
                if (first != null && second != null && (pair.Value == first || pair.Value == second))
                {
                    pair.Value.transform.GetChild(0).gameObject.SetActive(true);
                    //pair.Value.SpriteChanger(1);
                    //pair.Value.SpriteChanger(0);
                    //pair.Value.GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else
                {
                    pair.Value.transform.GetChild(0).gameObject.SetActive(false);
                    //pair.Value.SpriteChanger(0);
                    //pair.Value.GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }
        }
        
        
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
