using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmethystPanel : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private int _jump;
    private Vector3 _startPos;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPos = _rectTransform.localPosition;
    }

    public void ChangeAmethistValue(int value)
    {
        _text.text = value.ToString();
        if (value >= 0 && value <= 9) 
        {
            _rectTransform.localPosition = _startPos;
        }
        else if (value >= 10 && value <= 99)
        {
            _rectTransform.localPosition = new Vector3(_startPos.x + _jump, _startPos.y);
        }
        else if (value >=100 & value <= 999)
        {
            _rectTransform.localPosition = new Vector3(_startPos.x + _jump*2, _startPos.y);
        }
        else if (value >= 1000 & value <= 9999)
        {
            _rectTransform.localPosition = new Vector3(_startPos.x + _jump*3, _startPos.y);
        }
        else if (value >= 10000 & value <= 99999)
        {
            _rectTransform.localPosition = new Vector3(_startPos.x + _jump*4, _startPos.y);
        }
        else if (value >= 100000 & value <= 999999)
        {
            _rectTransform.localPosition = new Vector3(_startPos.x + _jump*5, _startPos.y);
        }
    }
}
