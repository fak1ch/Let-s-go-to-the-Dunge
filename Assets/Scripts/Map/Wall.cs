using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject block;

    public void ActivationDoor()
    {
        if (!block.GetComponent<Door>().mainRoom) 
        {
            block.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
