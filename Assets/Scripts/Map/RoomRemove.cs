using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRemove : MonoBehaviour
{
    private MainScript _mainScript;
    [SerializeField] private bool _mainRoom = false;

    private void Start()
    {
        _mainScript = StaticClass.mainScript;
        _mainScript.AddRoomToList(transform.root.gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_mainRoom)
        {
            if (collision.gameObject.CompareTag("RoomRemove"))
            {
                _mainScript.RemoveRoomFromList(transform.root.gameObject);
                Destroy(gameObject.transform.root.gameObject);
            }
        }
    }
}
