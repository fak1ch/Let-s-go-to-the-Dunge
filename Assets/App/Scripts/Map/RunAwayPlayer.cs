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

    private void Start()
    {
        _mainScript = StaticClass.mainScript;
        _player = StaticClass.player;
        transform.localPosition = new Vector3(0, 1.5f, 0);
        StartCoroutine(GetDistanceBetweenPlayerAndEnemy());
    }

    private void Update()
    {
        Vector3 vec = _mainScript.MainCamera.WorldToScreenPoint(_player.transform.position);
        Vector3 objectPos = _mainScript.MainCamera.WorldToScreenPoint(transform.position);
        vec.x = vec.x - objectPos.x;
        vec.y = vec.y - objectPos.y;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

        if (_distance >= _distanceWhenEnemyMoveToPlayer)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        else
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));
    }

    private IEnumerator GetDistanceBetweenPlayerAndEnemy()
    {
        _distance = Vector3.Distance(transform.root.position, _player.transform.position);
        yield return new WaitForSeconds(_timeBetweenGetDistance);
        StartCoroutine(GetDistanceBetweenPlayerAndEnemy());
    }
}
