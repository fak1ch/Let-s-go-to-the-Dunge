using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject openChest;
    private MoveHeroe moveHeroe;
    private MainScript mainScript;
    private bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        moveHeroe = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveHeroe>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   if (!locked)
        {
            if (collision != null)
            {
                if (collision.CompareTag("Player"))
                {
                    if (Input.GetKey(KeyCode.E) || moveHeroe.androidClickAction)
                    {
                        locked = true;
                        Instantiate(openChest, transform.position, Quaternion.identity);
                        SpawnWeapon();
                    }
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
        moveHeroe.allowPick = false;
        yield return new WaitForSeconds(1f);
        moveHeroe.GetComponent<MoveHeroe>().allowPick = true;
        Destroy(gameObject);
    }
}
