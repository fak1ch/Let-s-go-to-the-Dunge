using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayPlayer : MonoBehaviour
{
    [SerializeField] private Transform _pointMove;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetweenGetDistance =1f;
    [SerializeField] private float _distanceWhenEnemyMoveToPlayer = 500;
    private GameObject _player;
    private MainScript _mainScript;
    private float _distance;
    private Rigidbody2D _rb;

    private void Start()
    {
        _mainScript = StaticClass.mainScript;
        _player = StaticClass.player;
        _rb = transform.root.GetComponent<Rigidbody2D>();
        StartCoroutine(GetDistanceBetweenPlayerAndEnemy());
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    void Update()
    {
        if (_distance >= _distanceWhenEnemyMoveToPlayer)
        {
            Vector3 vec = _mainScript.camera.WorldToScreenPoint(_player.transform.position);
            Vector3 objectPos = _mainScript.camera.WorldToScreenPoint(transform.position);
            vec.x = vec.x - objectPos.x;
            vec.y = vec.y - objectPos.y;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            Vector3 vec = _mainScript.camera.WorldToScreenPoint(_player.transform.position);
            Vector3 objectPos = _mainScript.camera.WorldToScreenPoint(transform.position);
            vec.x = vec.x - objectPos.x;
            vec.y = vec.y - objectPos.y;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));
        }
    }

    public void EnemyMove()
    {
        if (_player.activeInHierarchy)
        {
            transform.root.position = Vector2.MoveTowards(transform.root.position, _pointMove.position, _speed * Time.deltaTime);
        }
    }

    private IEnumerator GetDistanceBetweenPlayerAndEnemy()
    {
        _distance = Vector3.Distance(transform.root.position, _player.transform.position);
        yield return new WaitForSeconds(_timeBetweenGetDistance);
        StartCoroutine(GetDistanceBetweenPlayerAndEnemy());
    }
}
