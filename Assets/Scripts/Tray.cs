using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, IInput, IAds
{
    public FailCondition Fail;
    public Flags Flag;
    public PopUpsSO PopUp;

    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 _defaultPos;
    private Vector2 _BaseTrayPos;
    private Vector2 _lastPos;
    private Vector2 _CheckCondtion;
    private Vector2 _childPos, _childTwoPos;

    private GameObject _child;
    private GameObject _childTwo;
    public GameObject ParentPanel;
    public GameObject SkipPopUp;
    public GameObject SpawnCircleArrowOne;
    public GameObject SpawnCircleArrowTwo;

    public RewardedAd RewardedAds;
    public LevelGeneration LevelGenerator;
    public SearchDirection searchDirection;
    public Skips Skip;

    [SerializeField] private GameObject _hex;
    [SerializeField] private Camera _cam;
    [SerializeField] private HexNode H1;
    [SerializeField] private HexNode H2;

    private void Start()
    {
        _CheckCondtion = new Vector3(-1, -1, -1);
        _BaseTrayPos = new Vector2(2, -4.75f);
        _defaultPos = new Vector2(2f, -4.75f);
        Fail.Fail = false;
        HexSpawner();
        searchDirection.Initialise();
    }

    public void Initialised(LevelGeneration _LevelGenerator, SearchDirection _searchDirection)
    {
        searchDirection = _searchDirection;
        LevelGenerator = _LevelGenerator;
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


    public void Tap(Touch touch)
    {
        RotateTile();
    }

    public void RotateTile()
    {
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
        if(transform.childCount == 1)
        {
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
        _startPos = transform.position;
        _endPos = _defaultPos;
        transform.position = _endPos;
    }

    public void GridFull()
    {
        foreach (var pair in LevelGenerator.HexDic)
        {
            if(pair.Value.IsGrab == false)
            {
                Fail.Fail = false;
                return;
            }
            Fail.Fail = true;
        }
    }

    public void Snap(Touch touch)
    {
        if (transform.childCount == 1)
        {
            var childTemp = _child.transform;
            _lastPos = LevelGenerator.GridFind(childTemp.position, Vector2.zero);
            if (_lastPos != _CheckCondtion)
            {
                childTemp.position = _lastPos;
                foreach (var i in LevelGenerator.HexDic)
                {
                    if (i.Value._locations == _lastPos)
                    {
                        childTemp.GetComponent<HexNode>().SetXY(i.Value.GetXY());
                        break;
                    }
                }

                childTemp.SetParent(LevelGenerator.transform);
                childTemp.GetComponent<HexNode>().ResetLayerOrder();

                HexSpawner();

                var x = (int)childTemp.GetComponent<HexNode>().GetXY().x;
                var y = (int)childTemp.GetComponent<HexNode>().GetXY().y;

                searchDirection.HexNodeArray[x, y] = childTemp.GetComponent<HexNode>();

                searchDirection.Visited.Clear();
                searchDirection.SearchNeighbours(childTemp.GetComponent<HexNode>());
                searchDirection.Merge();

            }
            else
            {
                Drop(touch);
            }
        }
        else if (transform.childCount > 1)
        {
            SnapTwo(touch);
        }
    }

    public void SnapTwo(Touch touch)
    {
        var childTemp = _child.transform;
        var childTwoTemp = _childTwo.transform;
        Vector2 delta = childTwoTemp.position - childTemp.position;
        
        _lastPos = LevelGenerator.GridFind(childTemp.position, delta);
        if(_lastPos != _CheckCondtion)
        {
            childTemp.position = _lastPos - delta;
            childTwoTemp.position = _lastPos;

            foreach (var i in LevelGenerator.HexDic)
            {
                if ((i.Value._locations - _lastPos).magnitude < 0.1f)
                {
                    childTwoTemp.GetComponent<HexNode>().SetXY(i.Value.GetXY());
                    break;
                }
            }
            foreach (var i in LevelGenerator.HexDic)
            {
                if ((i.Value._locations - (_lastPos - delta)).magnitude < 0.1f)
                {
                    childTemp.GetComponent<HexNode>().SetXY(i.Value.GetXY());
                    break;
                }
            }

            childTemp.SetParent(LevelGenerator.transform);
            childTwoTemp.SetParent(LevelGenerator.transform);

            childTemp.GetComponent<HexNode>().ResetLayerOrder();
            childTwoTemp.GetComponent<HexNode>().ResetLayerOrder();

            HexSpawner();

            var x = (int)childTemp.GetComponent<HexNode>().GetXY().x;
            var y = (int)childTemp.GetComponent<HexNode>().GetXY().y;

            searchDirection.HexNodeArray[x, y] = childTemp.GetComponent<HexNode>();

            x = (int)childTwoTemp.GetComponent<HexNode>().GetXY().x;
            y = (int)childTwoTemp.GetComponent<HexNode>().GetXY().y;

            searchDirection.HexNodeArray[x, y] = childTwoTemp.GetComponent<HexNode>();

            searchDirection.Visited.Clear();
            searchDirection.SearchNeighbours(childTemp.GetComponent<HexNode>());
            searchDirection.Merge();

            searchDirection.Visited.Clear();
            searchDirection.SearchNeighbours(childTwoTemp.GetComponent<HexNode>());
            searchDirection.Merge();

        }
        else
        {
            Drop(touch);
        }
        
    }

    public void Update()
    {
        GridFull();
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

            Hex.GetComponent<HexNode>().SortLayerOrder();
            HexTwo.GetComponent<HexNode>().SortLayerOrder();

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
            Hex.GetComponent<HexNode>().SortLayerOrder();
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

    public void SkipFunction()
    {
        if(Skip.GetSkips() == 0 )
        {
            Flag.InputFlag = true;
            ParentPanel.SetActive(true);
            SkipPopUp.SetActive(true);
        }
        else if(Skip.GetSkips() > 0)
        {
            SkipTile();
        }
    }

    public void SkipCancel()
    {
        Flag.InputFlag = false;
        SkipPopUp.SetActive(false);
        ParentPanel.SetActive(false);
    }

    public void SkipB()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            PopUp.EnablePopUp("Ad Not Available");
        }
        else
        {
            RewardedAds.ShowAd(this);
        }
        SkipCancel();
    }

    public void AdShown()
    {
        
    }

    public void AdClosed()
    {
        //SkipTile();
        Skip.SetSkips(1);
    }
}
