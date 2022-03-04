using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image[] _lives;
    [SerializeField] private Sprite _fullLive;
    [SerializeField] private Sprite _halfLive;
    [SerializeField] private Sprite _emptyLive;

    private int _health;
    private int _numberOfLives;
    private PlayerCharacteristic _playerCharacteristic;
    void Awake()
    {
        _playerCharacteristic = FindObjectOfType<PlayerCharacteristic>();
    }

    private void OnEnable()
    {
        _playerCharacteristic.OnHealthChange += UpdateHealthBar;
    }
    private void OnDisable()
    {
        _playerCharacteristic.OnHealthChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int health, int maxHealth)
    {
        _health = health;
        _numberOfLives = maxHealth;
        for (int i = 0; i < 2 * _lives.Length; i++)
        {
            int k = i % 2;
            double j = i / 2;
            Math.Floor(j);
            if (i < health)
            {
                _lives[(int)j].sprite = _fullLive;
            }
            if (i == health - 1 && k == 0)
            {
                _lives[(int)j].sprite = _halfLive;
            }
            if (i > health)
            {
                _lives[(int)j].sprite = _emptyLive;
            }

            if (i < _numberOfLives)
            {
                _lives[(int)j].enabled = true;
            }
            else
            {
                _lives[(int)j].enabled = false;
            }
        }
    }
}
