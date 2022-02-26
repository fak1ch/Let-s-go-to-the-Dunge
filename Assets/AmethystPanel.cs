using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmethystPanel : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private int _jumpText;
    private Vector3 _startPosText;
    private RectTransform _rectTransform;
    private PlayerCharacteristic _playerCharacteristic;

    void Awake()
    {
        _playerCharacteristic = FindObjectOfType<PlayerCharacteristic>();
        _rectTransform = GetComponent<RectTransform>();
        _startPosText = _rectTransform.localPosition;
    }

    private void OnEnable()
    {
        _playerCharacteristic.OnAmethystChange += ChangeAmethistValue;
    }

    private void OnDisable()
    {
        _playerCharacteristic.OnAmethystChange -= ChangeAmethistValue;
    }

    public void ChangeAmethistValue(int value)
    {
        _text.text = value.ToString();
        int digitCount = (int)Math.Log10(value);
        _rectTransform.localPosition = new Vector2(_startPosText.x + _jumpText*digitCount,_startPosText.y);
    }
}
