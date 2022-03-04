using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameOverMenuScript _gameOverMenu;
    private PlayerCharacteristic _playerCharacteristic;

    private void Awake()
    {
        _playerCharacteristic = FindObjectOfType<PlayerCharacteristic>();
    }

    private void OnEnable()
    {
        _playerCharacteristic.OnPlayerDie += OpenGameOverMenu;
    }

    private void OnDisable()
    {
        _playerCharacteristic.OnPlayerDie -= OpenGameOverMenu;
    }

    private void OpenGameOverMenu()
    {
        _gameOverMenu.OpenGameOverMenu();
    }
}
