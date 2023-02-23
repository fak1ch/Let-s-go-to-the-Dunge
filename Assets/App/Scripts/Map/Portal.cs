using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private GameEnd _gameEnd;
    private bool _isOpen = true;

    private void Start()
    {
        _gameEnd = FindObjectOfType<GameEnd>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isOpen)
        {
            if (collision.CompareTag("Player"))
            {
                _isOpen = false;
                _gameEnd.GameEndScreenOpen();
            }
        }
    }
}
