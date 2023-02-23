using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaParticle : MonoBehaviour
{
    [SerializeField]private  int _manacost;
    private bool _lockerOpen = true;
    private PlayerCharacteristic _playerCharacteristic;

    private void Start()
    {
        _playerCharacteristic = StaticClass.playerCharacteristic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_lockerOpen)
        {
            if (collision.CompareTag("Player"))
            {
                _lockerOpen = false;
                _playerCharacteristic.ChangeManaBar(_manacost);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (Vector3.Distance(_playerCharacteristic.transform.position, gameObject.transform.position) < 100)
        {
            transform.position = Vector3.Lerp(transform.position, _playerCharacteristic.transform.position, 6 * Time.deltaTime);
        }
    }
}
