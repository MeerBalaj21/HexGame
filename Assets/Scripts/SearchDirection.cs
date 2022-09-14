using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchDirection: MonoBehaviour
{
    public HexNode[] HexNodeArray;
    public int ID;
    private bool _found;
    public List<int> Visited;
    public int Count;
    public bool RowFive;
    public GameObject TempParent;

    private LevelGeneration _levelGeneration;

    public void Initialise(LevelGeneration _lG)
    {
        _levelGeneration = _lG;
    }
    public int Left()
    {
        return -5;
    }
    public int TopLeft()
    {
        if(RowFive == true)
        {
            return -4;
        }
        else
        {
            return 1;
        }
    }
    public int BottomLeft()
    {
        if (RowFive == true)
        {
            return -6;
        }
        else
        {
            return -1;
        }
    }
    public int Right()
    {
        return 5;
    }
    public int TopRight()
    {
        if (RowFive == true)
        {
            return 1;
        }
        else
        {
            return 6;
        }
    }
    public int BottomRight()
    {
        if (RowFive == true)
        {
            return -1;
        }
        else
        {
            return 4;
        }
    }

    public void Initialise()
    {
        HexNodeArray = new HexNode[25];
    }

    public void RowChecker(int index)
    {
        var num = index % 5;
        if(num == 0 || num == 2 || num == 4)
        {
            RowFive = true;
        }
        else if (num == 1 || num == 3)
        {
            RowFive = false;
        }
        else
        {
            Debug.Log("fault");
        }

    }

    public void Merge(int index)
    {
        SearchNeighbours(index);
        if (Visited.Count > 2)
        {
            Debug.Log("visited count" + Visited.Count);
            foreach (var i in Visited)
            {

                if (i != Visited[0])
                {
                    foreach(var x in _levelGeneration.HexDic)
                    {
                        if(x.Value.GetIndex() == i)
                        {
                            x.Value.ResetGrab();
                        }
                    }
                    HexNodeArray[i].transform.gameObject.SetActive(false);
                    HexNodeArray[i].transform.SetParent(TempParent.transform);
                    HexNodeArray[i] = null;
                }
            }

            var value = HexNodeArray[Visited[0]].Value;
            if (value != 3)
            {
                Debug.Log(value);
                HexNodeArray[Visited[0]].SpriteChanger(value + 1);
                HexNodeArray[Visited[0]].SetValue(value + 1);
            }

        }
        else
        {
            Debug.Log("visited count= " + Visited.Count);
        }


    }

    public void SearchNeighbours(int index)
    {
        if(HexNodeArray[index] == null)
        {
            return;
        }
        //Debug.Log(index);
        RowChecker(index);
        Visited.Add(index);

        if (!Visited.Contains(index + Left()))
        {
            if ((index + Left()) >= 0 && (index + Left()) <= 24)
            {
                if (HexNodeArray[index + Left()] != null)
                {
                    if (HexNodeArray[index].Value == HexNodeArray[index + Left()].Value)
                    {
                        //Debug.Log("VISITED = " + Visited[index+Left()]);
                        SearchNeighbours(index + Left());
                    }
                }
            } 
        }

        if (!Visited.Contains(index + TopLeft()))
        {
            if ((index + TopLeft()) >= 0 && (index + TopLeft()) <= 24)
            {
                if (HexNodeArray[index + TopLeft()] != null)
                {
                    if (HexNodeArray[index].Value == HexNodeArray[index + TopLeft()].Value)
                    {
                        //Debug.Log("VISITED = " + Visited[index]+TopLeft());
                        SearchNeighbours(index + TopLeft());
                    }
                }
            }
        }

        if (!Visited.Contains(index + TopRight()))
        {
            if ((index + TopRight()) >= 0 && (index + TopRight()) <= 24)
            {
                if (HexNodeArray[index + TopRight()] != null)
                {
                    if (HexNodeArray[index].Value == HexNodeArray[index + TopRight()].Value)
                    {
                        //Debug.Log("VISITED = " + Visited[index+TopRight()]);
                        SearchNeighbours(index + TopRight());
                    }
                }
            }        
        }
        
        if (!Visited.Contains(index + Right()))
        {
            if ((index + Right()) >= 0 && (index + Right()) <= 24)
            {
                if (HexNodeArray[index + Right()] != null)
                {
                    if (HexNodeArray[index].Value == HexNodeArray[index + Right()].Value)
                    {
                        //Debug.Log("VISITED = " + Visited[index+Right()]);
                        SearchNeighbours(index + Right());
                    }
                }
            }
        }
        
        if (!Visited.Contains(index + BottomRight()))
        {
            if ((index + BottomRight()) >= 0 && (index + BottomRight()) <= 24)
            {
                if (HexNodeArray[index + BottomRight()] != null)
                {
                    if (HexNodeArray[index].Value == HexNodeArray[index + BottomRight()].Value)
                    {
                        //Debug.Log("VISITED = " + Visited[index+BottomRight()]);
                        SearchNeighbours(index + BottomRight());
                    }
                }
            }
        }
        
        if (!Visited.Contains(index + BottomLeft()))
        {
            if ((index + BottomLeft()) >= 0 && (index + BottomLeft()) <= 24)
            {
                if (HexNodeArray[index + BottomLeft()] != null)
                {
                    if (HexNodeArray[index].Value == HexNodeArray[index + BottomLeft()].Value)
                    {
                        //Debug.Log("VISITED = " + Visited[index+BottomLeft()]);
                        SearchNeighbours(index + BottomLeft());
                    }
                }
            }
        }
        //else
        //{
        //    return;
        //}
        return;
    }


}
