using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _itemSprite;
    [SerializeField] private TypeOfItem _typeOfItem;
    [SerializeField] private GameObject _health;
    [SerializeField] private GameObject _mana;
    [SerializeField] private Text _priceText;

    private enum TypeOfItem { Health, Mana, Weapon }
    private GameObject _gameObjectItem;
    private bool _isLockerOpen = true;
    private WeaponsInventory _weaponsInventory;
    private int _price;
    private bool isOpen = true;
    private GameObject _weapon;

    void Start()
    {
        _weaponsInventory = StaticClass.weaponsInventory;
        _weapon = StaticClass.mainScript.kindOfWeapons[Random.Range(0, StaticClass.mainScript.kindOfWeapons.Count)];
        _health.GetComponent<HealthForHeal>().HealValue = 4;
        if (_typeOfItem == TypeOfItem.Weapon)
            CreateSpriteItem(_weapon, 200, 10);
        if (_typeOfItem == TypeOfItem.Health)
            CreateSpriteItem(_health, 100, 5);
        if (_typeOfItem == TypeOfItem.Mana)
            CreateSpriteItem(_mana, 200, 5);
        _priceText.text = _price.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_weaponsInventory.androidClickAction || Input.GetKeyDown(KeyCode.E))
            {
                if (_isLockerOpen && StaticClass.playerCharacteristic.Amethists >= _price)
                {
                    _isLockerOpen = false;
                    StaticClass.playerCharacteristic.TakeAmethyst(-_price);
                    var gm = Instantiate(_gameObjectItem, _itemSprite.gameObject.transform.position, Quaternion.identity);
                    if (_typeOfItem == TypeOfItem.Weapon) _weaponsInventory.PickGun(gm);

                    Destroy(GetComponent<CircleCollider2D>());
                    StaticClass.weaponsInventory.AllGunsGreenZoneState(false);
                    Destroy(_itemSprite.gameObject);
                    Destroy(_priceText.transform.parent.gameObject);
                }
            }
        }
    }

    private void CreateSpriteItem(GameObject item, float scale, int price)
    {
        _gameObjectItem = item;
        _itemSprite.sprite = item.GetComponent<SpriteRenderer>().sprite;
        _itemSprite.transform.localScale = _gameObjectItem.transform.localScale / scale;
        _price = price;
    }
}
