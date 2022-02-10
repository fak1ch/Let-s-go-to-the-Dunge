using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPanel : MonoBehaviour
{
    private Image image;
    private WeaponsInventory weaponsInventory;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        weaponsInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponsInventory>();
    }

    public void ChangeSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            image.enabled = true;
            image.sprite = sprite;
        }
        else
        {
            image.enabled = false;
        }
    }

    public void ChangeGun()
    {
        if (weaponsInventory.weapons.Count == 2)
        {
            if (weaponsInventory.activeGun == 1)
            {
                weaponsInventory.SelectGun(1);
            }
            else
            if (weaponsInventory.activeGun == 0)
            {
                weaponsInventory.SelectGun(2);
            }
        }
    }
}
