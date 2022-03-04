using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject _block;
    private Door _door;

    private void Start()
    {
        _door = _block.GetComponent<Door>();
    }

    public void ActivationDoor()
    {
        if (!_door.MainRoomFlag) 
        {
            _block.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
