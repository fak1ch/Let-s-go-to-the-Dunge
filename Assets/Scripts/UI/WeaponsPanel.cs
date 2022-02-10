using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPanel : MonoBehaviour
{
    private Image image;
    private MoveHeroe moveHeroe;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        moveHeroe = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveHeroe>();
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
        if (moveHeroe.Weapons.Count == 2)
        {
            if (moveHeroe.ActiveGun == 1)
            {
                moveHeroe.SelectGun(10);
            }
            else
            if (moveHeroe.ActiveGun == 0) 
            {
                moveHeroe.SelectGun(11);
            }
        }
    }
}
