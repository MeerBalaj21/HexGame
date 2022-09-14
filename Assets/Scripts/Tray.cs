using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, IInput, IAds
{
    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 _defaultPos = new Vector2(2f,-4.75f);
    private Vector2 _BaseTrayPos;
    private Vector3 _lastPos;
    private Vector3 _CheckCondtion;
    private GameObject _child;
    private GameObject _childTwo;
    private Vector2 _childPos, _childTwoPos;
    public LevelGeneration LevelGenerator;
    public GameObject SpawnCircleArrowOne;
    public GameObject SpawnCircleArrowTwo;
    public RewardedAd RewardedAds;
    public SearchDirection searchDirection;
    public Skips Skip;
    //[SerializeField] private int ID;
    [SerializeField] private GameObject _hex;
    [SerializeField] private Camera _cam;
    [SerializeField] private HexNode H1;
    [SerializeField] private HexNode H2;

    private void Start()
    {
        _CheckCondtion = new Vector3(-1, -1, -1);
        _BaseTrayPos = new Vector2(2, -4.75f);
        
        HexSpawner();
        //Initialise();
        searchDirection.Initialise();
    }

    public void Initialise()
    {
        if (transform.childCount > 1)
        {
            _child = transform.GetChild(0).gameObject;
            _childPos = new Vector2(-0.5f, 0f);
            _child.transform.localPosition = _childPos;

            _childTwo = transform.GetChild(1).gameObject;
            _childTwoPos = new Vector2(0.5f, 0f);
            _childTwo.transform.localPosition = _childTwoPos;

        }
        else
        {  
            _child = transform.GetChild(0).gameObject;
            _childPos = new Vector2(0f, 0f);
            _child.transform.localPosition = _childPos;
        }
    }

    public void Initialised(LevelGeneration _LevelGenerator, SearchDirection _searchDirection)
    {
        searchDirection = _searchDirection;
        LevelGenerator = _LevelGenerator;
    }

    public void Tap(Touch touch)
    {
        RotateTile();
    }

    public void RotateTile()
    {

        //Initialise();
        transform.Rotate(0f, 0f, -60f);
        if(transform.childCount == 1)
        {
            _child.transform.Rotate(0f, 0f, 60f);    
        }
        else
        {
            _child.transform.Rotate(0f, 0f, 60f);
            _childTwo.transform.Rotate(0f, 0f, 60f);
        }
    }
    public void Drag(Touch touch)
    {
        //Initialise();
        
        if(transform.childCount == 1)
        {
            //Vector2 offset = _cam.ScreenToWorldPoint(touch.position);
            //offset.y = offset.y + 1;
            //LevelGenerator.GridHighLight(offset, Vector2.zero);
            LevelGenerator.GridHighLight(_child.transform.position, Vector2.zero);
            transform.GetChild(0).GetComponent<HexNode>().SortLayerOrder();
        }
        else if( transform.childCount > 1)
        {
            Vector2 delta = _childTwo.transform.position - _child.transform.position;
            LevelGenerator.GridHighLight(_child.transform.position, delta);
            transform.GetChild(0).GetComponent<HexNode>().SortLayerOrder();
            transform.GetChild(1).GetComponent<HexNode>().SortLayerOrder();
        }

        var endPoint = _cam.ScreenToWorldPoint(touch.position);
        var dist = Vector2.Distance(_startPos, endPoint);
        _startPos = transform.position;
        _endPos = endPoint;
        _endPos.y = _endPos.y+1;
        transform.position = Vector2.Lerp(_startPos, _endPos, Time.deltaTime * 20 + dist);       
    }

    public void Drop(Touch touch)
    {
        var dist = Vector2.Distance(_startPos, _defaultPos);
        _startPos = transform.position;
        _endPos = _defaultPos;
        //transform.position = Vector2.Lerp(_startPos, _endPos, Time.deltaTime * 2 + dist);
        transform.position = _endPos;
    }

    public void Snap(Touch touch)
    {
        if (transform.childCount == 1)
        {
            var childTemp = _child;
            //Vector2 offset = _cam.ScreenToWorldPoint(touch.position);
            //offset.y = offset.y + 1;
            //_lastPos = (Vector3)LevelGenerator.GridFind(offset, Vector2.zero);
            _lastPos = (Vector3)LevelGenerator.GridFind(childTemp.transform.position, Vector2.zero);
            if (_lastPos != _CheckCondtion)
            {
                childTemp.transform.position = _lastPos;
                _child.transform.SetParent(LevelGenerator.transform);
                _child.transform.GetComponent<HexNode>().ResetLayerOrder();
                HexSpawner();
                //transform.position = _lastPos;

                //this method will find index and search using that
                int a = LevelGenerator.IndexFinder(_lastPos);
                if (a == -1)
                {
                    Drop(touch);
                    return;
                }
                else
                {
                    searchDirection.ID = a;
                }
                //this way ill store the xy position and seearch using them
                Vector2? temp = LevelGenerator.XYFinder(_lastPos);
                if (!temp.HasValue)
                {
                    Drop(touch);
                    return;
                }
                else
                {
                    childTemp.transform.GetComponent<HexNode>().SetXY((Vector2)temp);
                    //searchDirection.ID = a;
                }

                H1 = childTemp.GetComponent<HexNode>();
                searchDirection.HexNodeArray[searchDirection.ID] = H1;
                //searchDirection.HexNodeArray[searchDirection.xy] = H1;

                searchDirection.Visited.Clear();
                searchDirection.Merge(searchDirection.ID);


            }
            else
            {
                Drop(touch);
            }
        }
        else if (transform.childCount > 1)
        {
            Debug.Log("Snap two per ata hai");
            SnapTwo(touch);
            //testingSnapTwo(touch);
        }
        //testingSnapTwo(touch);
    }

    public void testingSnapTwo(Touch touch)
    {
        var childTemp = _child;
        var childTwoTemp = _childTwo;
        int ID = 0;
        Vector3 delta = childTwoTemp.transform.position - childTemp.transform.position;

        _lastPos = (Vector3)LevelGenerator.GridFind(childTemp.transform.position, Vector2.zero);
        if (_lastPos != _CheckCondtion)
        {
            childTemp.transform.position = _lastPos;
            _child.transform.SetParent(LevelGenerator.transform);
            _child.transform.GetComponent<HexNode>().ResetLayerOrder();

            int a = LevelGenerator.IndexFinder(_lastPos);
            if (a == -1)
            {
                Drop(touch);
                return;
            }
            else
            {
                searchDirection.ID = a;
            }
            ID = searchDirection.ID;
            H1 = childTemp.GetComponent<HexNode>();
            searchDirection.HexNodeArray[searchDirection.ID] = H1;

            _lastPos = (Vector3)LevelGenerator.GridFind(childTemp.transform.position + delta, Vector2.zero);
            if (_lastPos != _CheckCondtion)
            {
                childTwoTemp.transform.position = _lastPos;
                _childTwo.transform.SetParent(LevelGenerator.transform);
                _childTwo.transform.GetComponent<HexNode>().ResetLayerOrder();
                HexSpawner();
                //transform.position = _lastPos;

                a = LevelGenerator.IndexFinder(_lastPos);
                if (a == -1)
                {
                    Drop(touch);
                    return;
                }
                else
                {
                    searchDirection.ID = a;
                }

                H2 = childTwoTemp.GetComponent<HexNode>();
                searchDirection.HexNodeArray[searchDirection.ID] = H2;

                searchDirection.Visited.Clear();
                searchDirection.Merge(ID);

                searchDirection.Visited.Clear();
                searchDirection.Merge(searchDirection.ID);



            }
        }
        else
        {
            Drop(touch);
        }

        
    }

    public void SnapTwo(Touch touch)
    {
        //Initialise();
        var childTemp = _child;
        var childTwoTemp = _childTwo;
        Vector3 delta = childTwoTemp.transform.position - childTemp.transform.position;
        
        _lastPos = (Vector3)LevelGenerator.GridFind(childTemp.transform.position, delta);
        if(_lastPos != _CheckCondtion)
        {
            childTemp.transform.position = _lastPos - delta;
            childTwoTemp.transform.position = _lastPos;
            //_child.transform.position = _lastPos - (Vector3)delta;
            //_childTwo.transform.position = _lastPos;

            _child.transform.SetParent(LevelGenerator.transform);
            _childTwo.transform.SetParent(LevelGenerator.transform);
            _child.transform.GetComponent<HexNode>().ResetLayerOrder();
            _childTwo.transform.GetComponent<HexNode>().ResetLayerOrder();

            HexSpawner();
            //searchDirection.ID = (int)LevelGenerator.IndexFinder(_lastPos );
            //int? a = LevelGenerator.IndexFinder(_lastPos - (Vector3)delta);
            int a = LevelGenerator.IndexFinder(childTemp.transform.position);
            if (a == -1)
            {
                Drop(touch);
                return;
            }
            else
            {
                searchDirection.ID = a;
            }
            H1 = childTemp.GetComponent<HexNode>();
            searchDirection.HexNodeArray[searchDirection.ID] = H1;
            var ID = searchDirection.ID;

            //searchDirection.ID = (int)LevelGenerator.IndexFinder(_lastPos);
            a = LevelGenerator.IndexFinder(childTwoTemp.transform.position);
            if (a == -1)
            {
                Drop(touch);
                return;
            }
            else
            {
                searchDirection.ID = a;
            }
            H2 = childTwoTemp.GetComponent<HexNode>();
            searchDirection.HexNodeArray[searchDirection.ID] = H2;
            var IDTwo = searchDirection.ID;

            searchDirection.Visited.Clear();
            if(searchDirection.Visited.Count == 0)
            {
                searchDirection.Merge(ID);
                searchDirection.Visited.Clear();
                Debug.Log("Merge of 1 is called");
            }
            

            
            if(searchDirection.Visited.Count == 0)
            {
                searchDirection.Merge(IDTwo);
                Debug.Log("Merge of 2 is called");
            }
            

        }
        else
        {
            //Debug.Log("Else of the snap two function");
            Drop(touch);
        }
    }

    public void HexSpawner()
    {
       
        int ran = Random.Range(0, 2);
        int randomHex = Random.Range(0, 4);
        int randomHexTwo = Random.Range(0, 4);

        transform.position = _BaseTrayPos;
        if (ran == 1)
        {
            SpawnCircleArrowOne.SetActive(true);
            SpawnCircleArrowTwo.SetActive(true);
            GameObject Hex = (GameObject)Instantiate(_hex, _childPos, Quaternion.identity);
            GameObject HexTwo = (GameObject)Instantiate(_hex, _childTwoPos, Quaternion.identity);

            _child = Hex;
            _childTwo = HexTwo;

            Hex.transform.SetParent(this.transform);
            HexTwo.transform.SetParent(this.transform);

            var random = Random.Range(0, 3);
            for (int i = 0; i < random; i++)
            {
                RotateTile();
            }

            Hex.GetComponent<HexNode>().SpriteChanger(randomHex);
            Hex.GetComponent<HexNode>().SetValue(randomHex);
            HexTwo.GetComponent<HexNode>().SpriteChanger(randomHexTwo);
            HexTwo.GetComponent<HexNode>().SetValue(randomHexTwo);

            Hex.transform.localPosition = new Vector2(-0.5f, 0f);
            HexTwo.transform.localPosition = new Vector2(0.5f, 0f);
        }
        else
        {
            SpawnCircleArrowOne.SetActive(false);
            SpawnCircleArrowTwo.SetActive(false);
            GameObject Hex = (GameObject)Instantiate(_hex, _defaultPos, Quaternion.identity);
            _child = Hex;
            Hex.transform.SetParent(this.transform);
            Hex.GetComponent<HexNode>().SpriteChanger(randomHex);
            Hex.GetComponent<HexNode>().SetValue(randomHex);
            Hex.transform.localPosition = new Vector2(0f, 0f);

        }
        Initialise();
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
        if (transform.childCount == 1)
        {
            _child.transform.rotation = Quaternion.identity;

        }
        else
        {
            _child.transform.rotation = Quaternion.identity;
            _childTwo.transform.rotation = Quaternion.identity;
        }

    }

    public void SkipTile()
    {

        GameObject _childTemp = null;
        GameObject _childTwoTemp = null;

        if (transform.childCount == 1)
        {
            _childTemp = transform.GetChild(0).gameObject;
        }
        else
        {
            _childTemp = transform.GetChild(0).gameObject;
            _childTwoTemp = transform.GetChild(1).gameObject;    
        }

        transform.DetachChildren();
        if (transform.childCount == 1)
        {
            Destroy(_childTemp);       
        }
        else
        {
            Destroy(_childTemp);
            Destroy(_childTwoTemp);
        }

        HexSpawner();
        Skip.DecreaseSkip();

    }

    public void SkipB()
    {
        if (Skip.GetSkips() == 0)
        {
            RewardedAds.ShowAd(this);
        }
        else
        {
            SkipTile();
        }
    }

    public void AdShown()
    {
        
    }

    public void AdClosed()
    {
        SkipTile(); 
    }
}
