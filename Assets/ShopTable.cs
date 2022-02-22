using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _item;
    [SerializeField] private TypeOfItem _typeOfItem;
    [SerializeField] private GameObject _health;
    [SerializeField] private GameObject _mana;
    [SerializeField] private Text _priceText;

    private enum TypeOfItem { Health, Mana, Weapon }
    private GameObject _gameObjectItem;
    private MainScript _mainScript;
    private bool _isLockerOpen = true;
    private WeaponsInventory _weaponsInventory;
    private int _price;

    void Start()
    {
        _mainScript = StaticClass.mainScript;
        _weaponsInventory = StaticClass.weaponsInventory;
        if (_typeOfItem == TypeOfItem.Weapon)
        {
            _gameObjectItem = _mainScript.kindOfWeapons[Random.Range(0, _mainScript.kindOfWeapons.Count)];
            _item.sprite = _gameObjectItem.GetComponent<SpriteRenderer>().sprite;
            _item.transform.localScale = _gameObjectItem.transform.localScale / 200;
            _price = 10;
        }
        else if (_typeOfItem == TypeOfItem.Health)
        {
            _gameObjectItem = _health;
            _item.sprite = _gameObjectItem.GetComponent<SpriteRenderer>().sprite;
            _item.transform.localScale = _gameObjectItem.transform.localScale / 100;
            _gameObjectItem.GetComponent<HealthForHeal>().HealValue = 4;
            _price = 5;
        }
        else if (_typeOfItem == TypeOfItem.Mana)
        {
            _gameObjectItem = _mana;
            _item.sprite = _gameObjectItem.GetComponent<SpriteRenderer>().sprite;
            _item.transform.localScale = _gameObjectItem.transform.localScale / 200;
            _price = 5;
        }
        _priceText.text = _price.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_isLockerOpen && (_weaponsInventory.androidClickAction || Input.GetKeyDown(KeyCode.E)) && StaticClass.playerCharacteristic.Amethists >= _price)
            {
                _isLockerOpen = false;
                StaticClass.playerCharacteristic.TakeAmethyst(-_price);
                var gm = Instantiate(_gameObjectItem, _item.gameObject.transform.position, Quaternion.identity);
                if (_typeOfItem == TypeOfItem.Weapon) _weaponsInventory.PickGun(gm);

                Destroy(_item.gameObject);
            }
        }
    }
}
