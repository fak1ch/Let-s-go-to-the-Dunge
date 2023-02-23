using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _block;
    [SerializeField] private bool _mainRoom = false;
    [SerializeField] private bool _bossRoom = false;
    [SerializeField] private int _number;

    private bool _changed = false;
    private GameObject _otherRoom = null;
    private EnemySpawner _enemySpawner;

    public bool MainRoomFlag => _mainRoom;
    public int GetNumber => _number;

    private void Start()
    {
        _enemySpawner = transform.root.GetComponentInChildren<EnemySpawner>();
        StartCoroutine(ChangeDoor());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_changed)
        {
            if (collision.TryGetComponent(out Door door))
            {
                _changed = true;
                _otherRoom = door.transform.root.gameObject;
            }
            else if (collision.CompareTag("Wall"))
            {
                collision.gameObject.transform.parent.GetComponent<Wall>().ActivationDoor();
                _changed = true;
                _otherRoom = collision.gameObject.transform.root.gameObject;
            }
        }
    }

    private void CreateDoor()
    {
        _block.SetActive(false);
        gameObject.SetActive(true);
    }

    private void DeleteDoor()
    {
        _block.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator ChangeDoor()
    {
        yield return new WaitForSeconds(1f);
        if (_changed == false && !_bossRoom)
        {
            _changed = true;
            DeleteDoor();
        }
        if (_otherRoom == null && !_bossRoom)
        {
            DeleteDoor();
        }
        else if (_mainRoom)
        {
            CreateDoor();
            gameObject.SetActive(false);
        }
        else
        {
            if (!_bossRoom)
            {
                _enemySpawner.AddDoorsToList(gameObject);
                _enemySpawner.OpenDoors();
            }
        }
    }
}
