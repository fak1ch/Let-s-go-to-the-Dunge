using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction : MonoBehaviour
{
    private MoveHeroe moveHeroe;
    private void Start()
    {
        moveHeroe = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveHeroe>();
    }

    public void ClickMethod()
    {
        StartCoroutine(ClickEvent());
    }

    IEnumerator ClickEvent()
    {
        moveHeroe.androidClickAction = true;
        yield return new WaitForSeconds(0.1f);
        moveHeroe.androidClickAction = false;
    }
}
