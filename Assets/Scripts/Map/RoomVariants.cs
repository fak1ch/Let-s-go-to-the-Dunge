using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public bool mainRoomSpawn = true;

    public List<GameObject> topRooms;
    public List<GameObject> rightRooms;
    public List<GameObject> bottomRooms;
    public List<GameObject> leftRooms;

    public List<GameObject> topRooms1;
    public List<GameObject> rightRooms1;
    public List<GameObject> bottomRooms1;
    public List<GameObject> leftRooms1;

    public List<GameObject> mainRooms;
    public List<GameObject> bossRooms;

    public List<GameObject>[,] massiveVariants = new List<GameObject>[2, 4];

    [HideInInspector]public int numberOfMassive;

    private void Start()
    {
        massiveVariants[0, 0] = topRooms;
        massiveVariants[0, 1] = rightRooms;
        massiveVariants[0, 2] = bottomRooms;
        massiveVariants[0, 3] = leftRooms;
        massiveVariants[1, 0] = topRooms1;
        massiveVariants[1, 1] = rightRooms1;
        massiveVariants[1, 2] = bottomRooms1;
        massiveVariants[1, 3] = leftRooms1;
        numberOfMassive = Random.Range(0, massiveVariants.GetLength(0));
        CreateMainRoom();
    }

    private void CreateMainRoom()
    {
        if (mainRoomSpawn)
        {
            int i = Random.Range(0, mainRooms.Count);
            var obj = Instantiate(mainRooms[i], new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
        }
    }
}
