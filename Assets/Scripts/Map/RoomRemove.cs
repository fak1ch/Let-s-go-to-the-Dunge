using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRemove : MonoBehaviour
{
    private BoxCollider2D[] boxes;
    private GameObject doors;
    private GameObject room;
    private MainScript mainScript;
    public bool mainRoom = false;
    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.rooms.Add(transform.root.gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!mainRoom)
        {
            if (collision.gameObject.CompareTag("RoomRemove"))
            {
                mainScript.rooms.Remove(transform.root.gameObject);
                Destroy(gameObject.transform.root.gameObject);
            }
        }
    }
}
