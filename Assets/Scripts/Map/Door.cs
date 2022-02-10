using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public GameObject block;
    private bool changed = false;
    public bool mainRoom = false;
    public bool bossRoom = false;
    public int number;
    private GameObject otherRoom = null;

    private void Start()
    {
        StartCoroutine(ChangeDoor());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!changed)
        {
            if (collision.CompareTag("Door"))
            {
                changed = true;
                otherRoom = collision.gameObject.transform.root.gameObject;
            }
            else if (collision.CompareTag("Wall"))
            {
                collision.gameObject.transform.parent.GetComponent<Wall>().ActivationDoor();
                changed = true;
                otherRoom = collision.gameObject.transform.root.gameObject;
            }
        }
    }

    private void CreateDoor()
    {
        block.SetActive(false);
        gameObject.SetActive(true);
    }

    private void DeleteDoor()
    {
        block.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator ChangeDoor()
    {
        yield return new WaitForSeconds(1.5f);
        if (changed == false && !bossRoom)
        {
            changed = true;
            DeleteDoor();
        }
        if (otherRoom == null && !bossRoom)
        {
            DeleteDoor();
        }
        else if (mainRoom)
        {
            CreateDoor();
            gameObject.SetActive(false);
        }
        else
        {
            if (!bossRoom)
            {
                gameObject.transform.root.gameObject.GetComponentInChildren<EnemySpawner>().AddDoorsToList(gameObject);
                gameObject.transform.root.gameObject.GetComponentInChildren<EnemySpawner>().OpenDoors();
            }
        }
    }
}
