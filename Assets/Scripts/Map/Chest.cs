using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject openChest;
    private WeaponsInventory weaponsInventory;
    private MainScript mainScript;
    private bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        weaponsInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponsInventory>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (!locked)
        {
            if (collision.CompareTag("Player"))
            {
                if (Input.GetKey(KeyCode.E) || weaponsInventory.androidClickAction)
                {
                    locked = true;
                    Instantiate(openChest, transform.position, Quaternion.identity);
                    Destroy(GetComponent<CircleCollider2D>());
                    SpawnWeapon();
                }
            }
        }
    }

    private void SpawnWeapon()
    {
        Vector3 vec = transform.position;
        vec.y -= 10;
        Instantiate(mainScript.kindOfWeapons[Random.Range(0, mainScript.kindOfWeapons.Count)], vec,Quaternion.identity);
        StartCoroutine(AllowPick());
    }

    IEnumerator AllowPick()
    {
        weaponsInventory.allowPick = false;
        yield return new WaitForSeconds(0.2f);
        weaponsInventory.allowPick = true;
        Destroy(gameObject);
    }
}
