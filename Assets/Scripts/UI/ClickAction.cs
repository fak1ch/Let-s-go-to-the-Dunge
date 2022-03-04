using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction : MonoBehaviour
{
    private WeaponsInventory _weaponsInventory;
    private void Start()
    {
        _weaponsInventory = FindObjectOfType<WeaponsInventory>();
    }

    public void ClickMethod()
    {
        StartCoroutine(ClickEvent());
    }

    IEnumerator ClickEvent()
    {
        _weaponsInventory.AndroidClickAction = true;
        yield return new WaitForSeconds(0.1f);
        _weaponsInventory.AndroidClickAction = false;
    }
}
