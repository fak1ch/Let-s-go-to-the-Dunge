using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private float _startTimeBtwShots;

    private float _timeBtwShots;
    private int _hitsSpeedAttack;

    protected override void Start()
    {
        base.Start();
        SetMoveBehaviour(new NavMeshMoveToTarget(_navAgent, GetComponent<Animator>(), transform.position));
        _startTimeBtwShots = Random.Range(_startTimeBtwShots - 0.5f, _startTimeBtwShots + 0.5f);
        _timeBtwShots = _startTimeBtwShots;
    }

    protected override void Update()
    {
        base.Update();
        GunRotateToTarget();
        Shoot();
    }

    private void GunRotateToTarget()
    {
        if (_player.activeInHierarchy)
        {
            Vector3 vec = _mainScript.MainCamera.WorldToScreenPoint(_player.transform.position);
            Vector3 objectPos = _mainScript.MainCamera.WorldToScreenPoint(_shotPoint.position);
            vec.x = vec.x - objectPos.x;
            vec.y = vec.y - objectPos.y;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            _shotPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Shoot()
    {
        if (_player.activeInHierarchy)
        {
            if (_timeBtwShots <= 0)
            {
                ShotAudioPlay();
                var b = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);
                if (_hitsSpeedAttack == 5)
                {
                    _hitsSpeedAttack = 0;
                    Bullet bulletScript = b.GetComponentInChildren<Bullet>();
                    bulletScript.Speed *= 2;
                }
                else
                {
                    _hitsSpeedAttack++;
                }

                _timeBtwShots = _startTimeBtwShots;
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void ShotAudioPlay()
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource.Play();
        }
    }
}
