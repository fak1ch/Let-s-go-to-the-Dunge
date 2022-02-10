using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public List<GameObject> topRooms;
    public List<GameObject> rightRooms;
    public List<GameObject> bottomRooms;
    public List<GameObject> leftRooms;

    public List<GameObject> mainRooms;
    public List<GameObject> bossRooms;

    void Start()
    {
        CreateMainRoom();
    }

    private void CreateMainRoom()
    {
        int i = Random.Range(0, mainRooms.Count);
        Instantiate(mainRooms[i],new Vector3(0,0,0),Quaternion.identity);
    }
}
