using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchDirection: MonoBehaviour
{
    public HexNode[] HexNodeArray;
    public int ID;
    private bool _found;
    public List<HexNode> Visited;
    public int Count;
    public bool RowFive;

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



    public void SearchNeighbours(int index)
    {
        //Debug.Log(HexNodeArray[index]);
        //Debug.Log($"visited may ye para hua hai : {Visited}");
        RowChecker(index);
        Visited.Add(HexNodeArray[index]);
        if ((index + Left()) >= 0 && (index + Left()) <= 24 && HexNodeArray[index + Left()] != null
            && !Visited.Contains(HexNodeArray[index + Left()]) && HexNodeArray[index].Value == HexNodeArray[index + Left()].Value && Count <= 3)
        {
            _found = true;
     
            Count = Count + 1;
            Debug.Log($"index :{index}, Left: {Left()}, sum: {index + Left()}, Count: {Count}, found: {_found}");
            SearchNeighbours(index + Left());
        }

        if ((index + TopLeft()) >= 0 && (index + TopLeft()) <= 24 && HexNodeArray[index + TopLeft()] != null
            && !Visited.Contains(HexNodeArray[index + TopLeft()]) && HexNodeArray[index].Value == HexNodeArray[index + TopLeft()].Value && Count <= 3)
        {

            _found = true;
            //Visited.Add(HexNodeArray[index + TopLeft()]);
            Count = Count + 1;
            Debug.Log($"index :{index}, TopLeft: {TopLeft()}, sum: {index + TopLeft()}, Count: {Count}, found: {_found}");
            SearchNeighbours(index + TopLeft());
        }

        if ((index + TopRight()) >= 0 && (index + TopRight()) <= 24 && HexNodeArray[index + TopRight()] != null
            && !Visited.Contains(HexNodeArray[index + TopRight()]) && HexNodeArray[index].Value == HexNodeArray[index + TopRight()].Value && Count <= 3)
        {

            _found = true;
            //Visited.Add(HexNodeArray[index + TopRight()]);
            Count = Count + 1;
            Debug.Log($"index :{index}, TopRight: {TopRight()}, sum: {index + TopRight()}, Count: {Count}, found: {_found}");
            SearchNeighbours(index + TopRight());
        }
        
        if ((index + Right()) >= 0 && (index + Right()) <= 24 && HexNodeArray[index + Right()] != null
            && !Visited.Contains(HexNodeArray[index + Right()]) && HexNodeArray[index].Value == HexNodeArray[index + Right()].Value && Count <= 3)
        {
            _found = true;
            //Visited.Add(HexNodeArray[index + Right()]);
            Count = Count + 1;
            Debug.Log($"index :{index}, Right: {Right()}, sum: {index + Right()}, Count: {Count}, found: {_found}");
            SearchNeighbours(index + Right());
        }
        
        if ((index + BottomRight()) >= 0 && (index + BottomRight()) <= 24 && HexNodeArray[index + BottomRight()] != null
            && !Visited.Contains(HexNodeArray[index + BottomRight()]) &&  HexNodeArray[index].Value == HexNodeArray[index + BottomRight()].Value && Count <= 3)
        {
            _found = true;
            //Visited.Add(HexNodeArray[index + BottomRight()]);
            Count = Count + 1;
            Debug.Log($"index :{index}, BottomRight: {BottomRight()}, sum: {index + BottomRight()}, Count: {Count}, found: {_found}");
            SearchNeighbours(index + BottomRight());
        }
        
        if ((index + BottomLeft()) >= 0 && (index + BottomLeft()) <= 24 && HexNodeArray[index+BottomLeft()] != null
            && !Visited.Contains(HexNodeArray[index + BottomLeft()]) && HexNodeArray[index].Value == HexNodeArray[index + BottomLeft()].Value && Count <= 3)
        {
            _found = true;
            //Visited.Add(HexNodeArray[index + BottomLeft()]);
            Count = Count + 1;
            Debug.Log($"index :{index}, BottomLeft: {BottomLeft()}, sum: {index + BottomLeft()}, Count: {Count}, found: {_found}");
            SearchNeighbours(index + BottomLeft());
        }
        else
        {
            Debug.Log("found nothing");
            //Count = 0;
            _found = false;
            return;
        }
        Count = 0;
    }


}
