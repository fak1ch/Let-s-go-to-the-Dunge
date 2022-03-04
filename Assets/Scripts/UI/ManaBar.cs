using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Text _mana;
    [SerializeField] private Image _image;
    private PlayerCharacteristic _playerCharacteristic;

    private void Awake()
    {
        _playerCharacteristic = FindObjectOfType<PlayerCharacteristic>();
    }

    private void OnEnable()
    {
        _playerCharacteristic.OnManaChange += ChangeManaBar;
    }

    private void OnDisable()
    {
        _playerCharacteristic.OnManaChange -= ChangeManaBar;
    }

    private void ChangeManaBar(int mana, int maxMana)
    {
        _mana.text = $"{mana}/{maxMana}";
        float z = (float)mana / (float)maxMana;
        _image.fillAmount = z;
    }
}
