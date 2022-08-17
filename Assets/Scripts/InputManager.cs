using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TMP_Text DirectionText;
    private Touch _theTouch;
    private Vector2 _touchStartPosition, _touchEndPosition;
    private Vector3 _touchPosWorld;
    private Camera _camera;
    private string _direction;
    private RaycastHit2D _information;
    //public Vector2 Position;
    //private hexNode _tile;
    private void Start()
    {
        _camera = Camera.main;
    }


    private void Update()
    {
        if(Input.touchCount > 0)
        {
            _theTouch = Input.GetTouch(0);

            if(_theTouch.phase == TouchPhase.Began)
            {
                _touchStartPosition = _theTouch.position;

                _touchPosWorld = _camera.ScreenToWorldPoint(_touchStartPosition);
                _information = Physics2D.Raycast(_touchPosWorld, _camera.transform.forward, Mathf.Infinity);

                Debug.Log(_information);
                //if (!_information.collider) return;
            }
            if(!_information.collider.CompareTag("Hexagons"))
            {
                return;
            }
            //_tile = _information.transform.gameObject;
            else if(_theTouch.phase == TouchPhase.Moved || _theTouch.phase == TouchPhase.Ended)
            {
                _touchEndPosition = _theTouch.position;

                float x = _touchEndPosition.x - _touchStartPosition.x;
                float y = _touchEndPosition.y - _touchStartPosition.y;

                if(Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    _direction = "Tapped";
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    _direction = x > 0 ? "Right" : "Left";
                }
                else
                {
                    _direction = y > 0 ? "Up" : "Down";
                }
            }
        }
        DirectionText.text = _direction;
    }

}
