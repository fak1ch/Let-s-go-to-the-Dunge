using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        None,
        Top,
        Right,
        Bottom,
        Left
    }

    private RoomVariants variants;
    public bool spawned = false;
    private float waitTime = 3f;

    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        float i = Random.Range(0.150000000000f, 0.200000000000f);
        Invoke("Spawn", i);
    }

    public void Spawn()
    {
        if (!spawned)
        {
            if (direction == Direction.Top && variants.topRooms.Count !=0)
            {
                Instantiate(variants.topRooms[0], RoundVector3(transform.position), variants.topRooms[0].transform.rotation);
                variants.topRooms.RemoveAt(0);
            }
            else 
            if (direction == Direction.Right && variants.rightRooms.Count != 0)
            {
                Instantiate(variants.rightRooms[0], RoundVector3(transform.position), variants.rightRooms[0].transform.rotation);
                variants.rightRooms.RemoveAt(0);
            }
            else
            if (direction == Direction.Bottom && variants.bottomRooms.Count != 0)
            {
                Instantiate(variants.bottomRooms[0], RoundVector3(transform.position), variants.bottomRooms[0].transform.rotation);
                variants.bottomRooms.RemoveAt(0);
            }
            else
            if (direction == Direction.Left && variants.leftRooms.Count != 0)
            {
                Instantiate(variants.leftRooms[0], RoundVector3(transform.position), variants.leftRooms[0].transform.rotation);
                variants.leftRooms.RemoveAt(0);
            }
            spawned = true;
        }
    }

    private Vector3 RoundVector3(Vector3 vec)
    {
        int x = (int)vec.x;
        int y = (int)vec.y;
        int z = (int)vec.z;

        Vector3 result = new Vector3(x, y, z);
        return result;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }
}
