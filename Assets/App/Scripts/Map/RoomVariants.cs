using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    [SerializeField] private bool _mainRoomSpawn = true;

    [SerializeField] private List<GameObject> _topRooms;
    [SerializeField] private List<GameObject> _rightRooms;
    [SerializeField] private List<GameObject> _bottomRooms;
    [SerializeField] private List<GameObject> _leftRooms;

    [SerializeField] private List<GameObject> _topRooms1;
    [SerializeField] private List<GameObject> _rightRooms1;
    [SerializeField] private List<GameObject> _bottomRooms1;
    [SerializeField] private List<GameObject> _leftRooms1;

    [SerializeField] private List<GameObject> _mainRooms;
    [SerializeField] private List<GameObject> _bossRooms;
    [SerializeField] private List<GameObject> _shopRooms;

    private List<GameObject>[,] _massiveVariants = new List<GameObject>[2, 4];
    private int _numberOfMassive;

    public List<GameObject>[,] MassiveVariants => _massiveVariants;
    public int NumberOfMassive => _numberOfMassive;

    private void Start()
    {
        _massiveVariants[0, 0] = _topRooms;
        _massiveVariants[0, 1] = _rightRooms;
        _massiveVariants[0, 2] = _bottomRooms;
        _massiveVariants[0, 3] = _leftRooms;
        _massiveVariants[1, 0] = _topRooms1;
        _massiveVariants[1, 1] = _rightRooms1;
        _massiveVariants[1, 2] = _bottomRooms1;
        _massiveVariants[1, 3] = _leftRooms1;
        _numberOfMassive = Random.Range(0, _massiveVariants.GetLength(0));
        CreateMainRoom();
    }

    private void CreateMainRoom()
    {
        if (_mainRoomSpawn)
        {
            int i = Random.Range(0, _mainRooms.Count);
            Instantiate(_mainRooms[i], new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
        }
    }

    public GameObject GetMainRoomByIndex(int index)
    {
        return _mainRooms[index];
    }

    public GameObject GetBossRoomByIndex(int index)
    {
        return _bossRooms[index];
    }

    public GameObject GetShopRoomByIndex(int index)
    {
        return _shopRooms[index];
    }
}
