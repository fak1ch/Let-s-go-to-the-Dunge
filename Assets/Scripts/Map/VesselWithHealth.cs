using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselWithHealth : MonoBehaviour
{
    public bool mostPercent = false;
    public GameObject healHealth;

    private bool isOpen = true;

    public void SpawnHealth()
    {
        if (mostPercent)
        {
            if (Random.Range(0,2) == 0)
            {
                Instantiate(healHealth, gameObject.transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (Random.Range(0, 4) == 0)
            {
                Instantiate(healHealth, gameObject.transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet") && isOpen)
        {
            isOpen = false;
            SpawnHealth();
        }
    }
}
