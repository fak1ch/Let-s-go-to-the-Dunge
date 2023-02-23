using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private Direction _direction;

    private RoomVariants _variants;
    private bool _spawned = false;
    private float _waitTime = 3f;
    public enum Direction
    {
        None,
        Top,
        Right,
        Bottom,
        Left
    }

    void Start()
    {
        _variants = FindObjectOfType<RoomVariants>();
        Destroy(gameObject, _waitTime);
        float i = Random.Range(0.150000000000f, 0.200000000000f);
        Invoke("Spawn", i);
    }

    public void Spawn()
    {
        if (!_spawned)
        {
            int k = _variants.NumberOfMassive;
            if (_direction == Direction.Top && _variants.MassiveVariants[k,0].Count !=0)
            {
                Instantiate(_variants.MassiveVariants[k, 0][0], RoundVector3(transform.position), _variants.MassiveVariants[k, 0][0].transform.rotation);
                _variants.MassiveVariants[k, 0].RemoveAt(0);
            }
            else 
            if (_direction == Direction.Right && _variants.MassiveVariants[k, 1].Count != 0)
            {
                Instantiate(_variants.MassiveVariants[k, 1][0], RoundVector3(transform.position), _variants.MassiveVariants[k, 1][0].transform.rotation);
                _variants.MassiveVariants[k, 1].RemoveAt(0);
            }
            else
            if (_direction == Direction.Bottom && _variants.MassiveVariants[k, 2].Count != 0)
            {
                Instantiate(_variants.MassiveVariants[k, 2][0], RoundVector3(transform.position), _variants.MassiveVariants[k, 2][0].transform.rotation);
                _variants.MassiveVariants[k, 2].RemoveAt(0);
            }
            else
            if (_direction == Direction.Left && _variants.MassiveVariants[k, 3].Count != 0)
            {
                Instantiate(_variants.MassiveVariants[k, 3][0], RoundVector3(transform.position), _variants.MassiveVariants[k, 3][0].transform.rotation);
                _variants.MassiveVariants[k, 3].RemoveAt(0);
            }
            _spawned = true;
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

    public bool GetFlagSpawned()
    {
        return _spawned;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().GetFlagSpawned())
        {
            Destroy(gameObject);
        }
    }
}
