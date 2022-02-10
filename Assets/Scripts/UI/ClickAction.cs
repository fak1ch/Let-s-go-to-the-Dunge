using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction : MonoBehaviour
{
    private WeaponsInventory weaponsInventory;
    private void Start()
    {
        weaponsInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponsInventory>();
    }

    public void ClickMethod()
    {
        StartCoroutine(ClickEvent());
    }

    IEnumerator ClickEvent()
    {
        weaponsInventory.androidClickAction = true;
        yield return new WaitForSeconds(0.1f);
        weaponsInventory.androidClickAction = false;
    }
}
