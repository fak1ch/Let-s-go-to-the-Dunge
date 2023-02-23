using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject _openChest;
    private WeaponsInventory _weaponsInventory;
    private MainScript _mainScript;
    private bool _locked = false;

    void Start()
    {
        _mainScript = FindObjectOfType<MainScript>();
        _weaponsInventory = FindObjectOfType<WeaponsInventory>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (!_locked)
        {
            if (collision.CompareTag("Player"))
            {
                if (Input.GetKey(KeyCode.E) || _weaponsInventory.AndroidClickAction)
                {
                    _locked = true;
                    Instantiate(_openChest, transform.position, Quaternion.identity);
                    SpawnWeapon();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void SpawnWeapon()
    {
        Vector3 vec = transform.position;
        vec.y -= 10;
        Instantiate(_mainScript.GetRandomWeapon(), vec, Quaternion.identity);
        _weaponsInventory.AllowPiclStartCorutine(0.2f);
    }
}
