using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchDirection: MonoBehaviour
{
    public HexNode[,] HexNodeArray;
    public List<Vector2> Visited;
    public GameObject NewParent;
    public LevelGeneration LevelGenerator;
    public bool RowFiveTemp;

    public void Initialise()
    {
        HexNodeArray = new HexNode[5,5];
    }

    public void Initialise(LevelGeneration _lG)
    {
        LevelGenerator = _lG;
    }

    public void RowChecker(HexNode Hex)
    {
        if(Hex.GetXY().y == 0 || Hex.GetXY().y == 2 || Hex.GetXY().y == 4)
        {
            Hex.RowFive = true;
            RowFiveTemp = Hex.RowFive;
        }
        else if (Hex.GetXY().y == 1 || Hex.GetXY().y == 3)
        {
            Hex.RowFive = false;
            RowFiveTemp = Hex.RowFive;
        }
    }

    public Vector2 Left()
    {
        return new Vector2(-1, 0);
    }

    public Vector2 TopLeft()
    {
        if(RowFiveTemp == true)
        {
            return new Vector2(-1, 1);
        }
        return new Vector2(0, 1);    
    }

    public Vector2 TopRight()
    {
        if (RowFiveTemp == true)
        {
            return new Vector2(0, 1);
        }
        return new Vector2(1, 1);
    }

    public Vector2 Right()
    {
        return new Vector2(1, 0);
    }

    public Vector2 BottomRight()
    {
        if (RowFiveTemp == true)
        {
            return new Vector2(0,-1);
        }
        return new Vector2(1, -1);
    }

    public Vector2 BottomLeft()
    {
        if (RowFiveTemp == true)
        {
            return new Vector2(-1, -1);
        }
        return new Vector2(0, -1);
    }

    public void Merge()
    {
        if(Visited.Count > 2)
        {
            for(int i = 1; i < Visited.Count; i++)
            {
                foreach(var pair in LevelGenerator.HexDic)
                {
                    if (pair.Value.GetXY() == Visited[i])
                    {
                        pair.Value.ResetGrab();
                    }
                }
                HexNodeArray[(int)Visited[i].x, (int)Visited[i].y].gameObject.SetActive(false);
                HexNodeArray[(int)Visited[i].x, (int)Visited[i].y].gameObject.transform.SetParent(NewParent.transform);
                HexNodeArray[(int)Visited[i].x, (int)Visited[i].y] = null;
            }

            var Value = HexNodeArray[(int)Visited[0].x, (int)Visited[0].y].Value;
            if (Value != 3)
            {
                HexNodeArray[(int)Visited[0].x, (int)Visited[0].y].SetValue(Value + 1);
                HexNodeArray[(int)Visited[0].x, (int)Visited[0].y].SpriteChanger(Value + 1);
            }
        }
        return;
    }

    public void SearchNeighbours(HexNode Hex)
    {
        var x = (int)Hex.GetXY().x;
        var y = (int)Hex.GetXY().y;

        if (HexNodeArray[x, y] == null)
        {
            return;
        }

        RowChecker(Hex);

        Visited.Add(Hex.GetXY());

        if (!Visited.Contains(Hex.GetXY() + Left()))
        {
            if ((x + Left().x) >= 0  && (x + Left().x) <= 4)
            {
                if ((y + Left().y) >= 0 && (y + Left().y) <= 4)
                {
                    if (HexNodeArray[x + (int)Left().x, y + (int)Left().y] != null)
                    {
                        if (HexNodeArray[x, y].Value == HexNodeArray[x + (int)Left().x, y + (int)Left().y].Value)
                        {
                            SearchNeighbours(HexNodeArray[x + (int)Left().x, y + (int)Left().y]);
                        }
                    }
                }
            }
        }

        if (!Visited.Contains(Hex.GetXY() + TopLeft()))
        {
            if ((x + TopLeft().x) >= 0 && (x + TopLeft().x) <= 4)
            {
                if ((y + TopLeft().y) >= 0 && (y + TopLeft().y) <= 4)
                {
                    if (HexNodeArray[x + (int)TopLeft().x, y + (int)TopLeft().y] != null)
                    {
                        if (HexNodeArray[x, y].Value == HexNodeArray[x + (int)TopLeft().x, y + (int)TopLeft().y].Value)
                        {
                            SearchNeighbours(HexNodeArray[x + (int)TopLeft().x, y + (int)TopLeft().y]);
                        }
                    }
                }
            }
        }

        if (!Visited.Contains(Hex.GetXY() + TopRight()))
        {
            if ((x + TopRight().x) >= 0 && (x + TopRight().x) <= 4)
            {
                if ((y + TopRight().y) >= 0 && (y + TopRight().y) <= 4)
                {
                    if (HexNodeArray[x + (int)TopRight().x, y + (int)TopRight().y] != null)
                    {
                        if (HexNodeArray[x, y].Value == HexNodeArray[x + (int)TopRight().x, y + (int)TopRight().y].Value)
                        {
                            SearchNeighbours(HexNodeArray[x + (int)TopRight().x, y + (int)TopRight().y]);
                        }
                    }
                }
            }
        }

        if (!Visited.Contains(Hex.GetXY() + Right()))
        {
            if ((x + Right().x) >= 0 && (x + Right().x) <= 4)
            {
                if ((y + Right().y) >= 0 && (y + Right().y) <= 4)
                {
                    if (HexNodeArray[x + (int)Right().x, y + (int)Right().y] != null)
                    {
                        if (HexNodeArray[x, y].Value == HexNodeArray[x + (int)Right().x, y + (int)Right().y].Value)
                        {
                            SearchNeighbours(HexNodeArray[x + (int)Right().x, y + (int)Right().y]);
                        }
                    }
                }
            }
        }

        if (!Visited.Contains(Hex.GetXY() + BottomRight()))
        {
            if ((x + BottomRight().x) >= 0 && (x + BottomRight().x) <= 4)
            {
                if ((y + BottomRight().y) >= 0 && (y + BottomRight().y) <= 4)
                {
                    if (HexNodeArray[x + (int)BottomRight().x, y + (int)BottomRight().y] != null)
                    {
                        if (HexNodeArray[x, y].Value == HexNodeArray[x + (int)BottomRight().x, y + (int)BottomRight().y].Value)
                        {
                            SearchNeighbours(HexNodeArray[x + (int)BottomRight().x, y + (int)BottomRight().y]);
                        }
                    }
                }
            }
        }

        if (!Visited.Contains(Hex.GetXY() + BottomLeft()))
        {
            if ((x + BottomLeft().x) >= 0 && (x + BottomLeft().x) <= 4)
            {
                if ((y + BottomLeft().y) >= 0 && (y + BottomLeft().y) <= 4)
                {
                    if (HexNodeArray[x + (int)BottomLeft().x, y + (int)BottomLeft().y] != null)
                    {
                        if (HexNodeArray[x, y].Value == HexNodeArray[x + (int)BottomLeft().x, y + (int)BottomLeft().y].Value)
                        {
                            SearchNeighbours(HexNodeArray[x + (int)BottomLeft().x, y + (int)BottomLeft().y]);
                        }
                    }
                }
            }
        }
        return;
    }
}
