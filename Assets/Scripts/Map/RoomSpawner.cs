using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    public RoomVariants variants;

    public enum Direction
    {
        None,
        Top,
        Right,
        Bottom,
        Left
    }

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
            int k = variants.numberOfMassive;
            if (direction == Direction.Top && variants.massiveVariants[k,0].Count !=0)
            {
                Instantiate(variants.massiveVariants[k, 0][0], RoundVector3(transform.position), variants.massiveVariants[k, 0][0].transform.rotation);
                variants.massiveVariants[k, 0].RemoveAt(0);
            }
            else 
            if (direction == Direction.Right && variants.massiveVariants[k, 1].Count != 0)
            {
                Instantiate(variants.massiveVariants[k, 1][0], RoundVector3(transform.position), variants.massiveVariants[k, 1][0].transform.rotation);
                variants.massiveVariants[k, 1].RemoveAt(0);
            }
            else
            if (direction == Direction.Bottom && variants.massiveVariants[k, 2].Count != 0)
            {
                Instantiate(variants.massiveVariants[k, 2][0], RoundVector3(transform.position), variants.massiveVariants[k, 2][0].transform.rotation);
                variants.massiveVariants[k, 2].RemoveAt(0);
            }
            else
            if (direction == Direction.Left && variants.massiveVariants[k, 3].Count != 0)
            {
                Instantiate(variants.massiveVariants[k, 3][0], RoundVector3(transform.position), variants.massiveVariants[k, 3][0].transform.rotation);
                variants.massiveVariants[k, 3].RemoveAt(0);
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
