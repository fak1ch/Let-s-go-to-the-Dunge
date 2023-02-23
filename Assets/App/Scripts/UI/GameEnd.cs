using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private GameObject _gameEndScreen;

    public void GameEndScreenOpen()
    {
        _gameEndScreen.SetActive(true);
    }
}
