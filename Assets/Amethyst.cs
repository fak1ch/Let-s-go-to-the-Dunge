using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amethyst : MonoBehaviour
{
    [SerializeField]private int _amountAmetists;
    private PlayerCharacteristic _playerCharacteristic;
    private bool _isLockerOpen = true;

    void Start()
    {
        _playerCharacteristic = StaticClass.playerCharacteristic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_isLockerOpen)
            {
                _isLockerOpen = false;
                _playerCharacteristic.TakeAmethyst(_amountAmetists);
                Destroy(gameObject);
            }
        }
    }
}
