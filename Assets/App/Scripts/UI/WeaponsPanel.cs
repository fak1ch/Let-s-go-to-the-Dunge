using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPanel : MonoBehaviour
{
    private Image _image;
    private WeaponsInventory _weaponsInventory;

    private void OnEnable()
    {
        _weaponsInventory.OnWeaponChange += ChangeSprite;
    }

    private void OnDisable()
    {
        _weaponsInventory.OnWeaponChange -= ChangeSprite;
    }

    void Awake()
    {
        _image = gameObject.GetComponent<Image>();
        _weaponsInventory = FindObjectOfType<WeaponsInventory>();
    }

    public void ChangeSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            _image.enabled = true;
            _image.sprite = sprite;
        }
        else
        {
            _image.enabled = false;
        }
    }

    public void ChangeGun()
    {
        if (_weaponsInventory.WeaponsCount == 2)
        {
            if (_weaponsInventory.ActiveGun == 1)
            {
                _weaponsInventory.SelectGun(1);
            }
            else
            if (_weaponsInventory.ActiveGun == 0)
            {
                _weaponsInventory.SelectGun(2);
            }
        }
    }
}
