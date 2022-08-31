using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private int id;
    public GameObject Hex;
    private HexNode _hex;

    [SerializeField] private Dictionary<string, HexNode> HexDic = new Dictionary<string, HexNode>();
    //[SerializeField] private Dictionary<int, HexNode> HexDic = new Dictionary<int, HexNode>();
    //[SerializeField] public Dictionary<int, Vector2> ReturnDic = new Dictionary<int, Vector2>();

    private void Start()
    {
        GridSetup();
    }

    public Vector3 GridFind(Vector2 pos, Vector2 delta)
    {

        if (delta == Vector2.zero)
        {
            foreach (var pair in HexDic)
            {
                float distance = Vector2.Distance(pos, pair.Value._locations);
                if (distance <= 0.5f && (pair.Value.IsGrab == false))
                {
                    Vector2 loc = pair.Value._locations;
                    pair.Value.SetGrab();
                    Debug.Log("delta zero");
                    pair.Value.transform.GetChild(0).gameObject.SetActive(false);
                    return loc;
                }
            }
        }
        else if (delta != Vector2.zero)
        {
            foreach (var pair in HexDic)
            {
                float distance = Vector2.Distance(pos, pair.Value._locations);
                if (distance <= 0.5f && (pair.Value.IsGrab == false))
                {
                    Vector2 loc = pair.Value._locations;

                    loc = loc + delta;

                    foreach (var secondPair in HexDic)
                    {
                        if ((loc - secondPair.Value._locations).magnitude < 0.49 && secondPair.Value.IsGrab == false)
                        {
                            Debug.Log("found" + loc);
                            pair.Value.SetGrab();
                            secondPair.Value.SetGrab();

                            pair.Value.transform.GetChild(0).gameObject.SetActive(false);
                            secondPair.Value.transform.GetChild(0).gameObject.SetActive(false);
                            return loc;
                        }
                    }
                }
            }
        }
        return new Vector3(-1, -1, -1);
    }

    public int? IndexFinder(Vector2 loc)
    {
        foreach( var pair in HexDic)
        {
            //Debug.LogError($"{pair.Value._locations.x} {pair.Value._locations.y} : {loc.x} {loc.y} = {pair.Value._locations.x == loc.x && pair.Value._locations.y.ToString("0.00") == loc.y.ToString("0.00")}");
            if(pair.Value._locations.x == loc.x && pair.Value._locations.y.ToString("0.00") == loc.y.ToString("0.00"))
            {
                //Debug.Log($"{pair.Value.GetIndex()}");
                return pair.Value.GetIndex();
            }
            else
            {
                //Debug.Log("else of index finder");
                continue;
            }
        }
        //Debug.Log("end of index finder");
        return null;
    }

    public void GridHighLight(Vector2 pos, Vector2 delta)
    {
        if(delta == Vector2.zero)
        {
            foreach (var pair in HexDic)
            {
                float distance = Vector2.Distance(pos, pair.Value._locations);
                if (distance <= 0.5f && (pair.Value.IsGrab == false))
                {
                    pair.Value.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (distance > 0.5f)
                {
                    pair.Value.transform.GetChild(0).gameObject.SetActive(false);
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
                if (distance <= 0.5f && (pair.Value.IsGrab == false))
                {
                    first = pair.Value;

                    Vector2 loc = pair.Value._locations;
                    loc = loc + delta;
                    foreach (var secondPair in HexDic)
                    {
                        if (first != secondPair.Value)
                        {
                            if ((loc - secondPair.Value._locations).magnitude < 0.49 && secondPair.Value.IsGrab == false)
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
                }
                else
                {
                    pair.Value.transform.GetChild(0).gameObject.SetActive(false);
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
                //_hex.SetValue(0);
                _hex.SetIndex(id);
               
                HexDic.Add(name, _hex);
                //HexDic.Add(id, _hex);

                HexGo.transform.SetParent(this.transform);
                id = id + 1;
            }
        }
    }
}
