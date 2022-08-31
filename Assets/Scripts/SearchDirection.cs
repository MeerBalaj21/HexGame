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
                    HexNodeArray[i] = null;
                }
            }

            var value = HexNodeArray[Visited[0]].Value;
            if (value != 3)
            {
                Debug.Log(value);
                HexNodeArray[Visited[0]].SpriteChanger(value + 1);
            }

        }
        else
        {
            Debug.Log("visited count" + Visited.Count);
        }


    }

    public void SearchNeighbours(int index)
    {
        if(HexNodeArray[index] == null)
        {
            return;
        }
        Debug.Log(index);
        RowChecker(index);
        Visited.Add(index);
        if ((index + Left()) >= 0 && (index + Left()) <= 24 && HexNodeArray[index + Left()] != null
            && !Visited.Contains(index + Left()) && HexNodeArray[index].Value == HexNodeArray[index + Left()].Value)
        {
            _found = true;
            //Count = Count + 1;
            SearchNeighbours(index + Left());
        }

        if ((index + TopLeft()) >= 0 && (index + TopLeft()) <= 24 && HexNodeArray[index + TopLeft()] != null
            && !Visited.Contains(index + TopLeft()) && HexNodeArray[index].Value == HexNodeArray[index + TopLeft()].Value)
        {

            _found = true;
            //Count = Count + 1;
            SearchNeighbours(index + TopLeft());
        }

        if ((index + TopRight()) >= 0 && (index + TopRight()) <= 24 && HexNodeArray[index + TopRight()] != null
            && !Visited.Contains(index + TopRight()) && HexNodeArray[index].Value == HexNodeArray[index + TopRight()].Value)
        {

            _found = true;
            //Count = Count + 1;
            SearchNeighbours(index + TopRight());
        }
        
        if ((index + Right()) >= 0 && (index + Right()) <= 24 && HexNodeArray[index + Right()] != null
            && !Visited.Contains(index + Right()) && HexNodeArray[index].Value == HexNodeArray[index + Right()].Value)
        {
            _found = true;
            //Count = Count + 1;
            SearchNeighbours(index + Right());
        }
        
        if ((index + BottomRight()) >= 0 && (index + BottomRight()) <= 24 && HexNodeArray[index + BottomRight()] != null
            && !Visited.Contains(index + BottomRight()) &&  HexNodeArray[index].Value == HexNodeArray[index + BottomRight()].Value)
        {
            _found = true;
            //Count = Count + 1;
            SearchNeighbours(index + BottomRight());
        }
        
        if ((index + BottomLeft()) >= 0 && (index + BottomLeft()) <= 24 && HexNodeArray[index+BottomLeft()] != null
            && !Visited.Contains(index + BottomLeft()) && HexNodeArray[index].Value == HexNodeArray[index + BottomLeft()].Value)
        {
            _found = true;
            //Count = Count + 1;
            SearchNeighbours(index + BottomLeft());
        }
        else
        {
            Debug.Log("found nothing");
            _found = false;
            return;
        }
        
    }


}
