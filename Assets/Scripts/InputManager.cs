using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TMP_Text PhaseTextDisplay;
    private Touch _theTouch;
    private float _timeTouchEnded;
    private float _displayTime = 0.5f;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            _theTouch = Input.GetTouch(0);
            if(_theTouch.phase == TouchPhase.Ended)
            {
                PhaseTextDisplay.text = _theTouch.phase.ToString();
                _timeTouchEnded = Time.time;
            }
            else if(Time.time - _timeTouchEnded > _displayTime)
            {
                PhaseTextDisplay.text = _theTouch.phase.ToString();
                _timeTouchEnded = Time.time;
            }
        }
        else if(Time.time - _timeTouchEnded > _displayTime)
        {
            PhaseTextDisplay.text = " ";
        }
    }

}
