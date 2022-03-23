using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private float _timeBtwShots;

    private RotateToTarget _rotateToTarget;
    private int _hitsSpeedAttack;
    private bool _allowShoot = false;

    protected override void Start()
    {
        base.Start();
        SetMoveBehaviour(new NavMeshMoveToTarget(_navAgent, GetComponent<Animator>(), transform.position));
        _rotateToTarget = new RotateToTarget(_mainScript.MainCamera);
        _timeBtwShots = Random.Range(_timeBtwShots - 0.5f, _timeBtwShots + 0.5f);
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
            _rotateToTarget.Rotate(_player.transform.position, transform);
        }
    }

    private void Shoot()
    {
        if (_player.activeInHierarchy)
        {
            if (_allowShoot)
            {
                StartCoroutine(ShootCooldown());
                ShotAudioPlay();
                var b = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);
                _hitsSpeedAttack++;

                if (_hitsSpeedAttack == 5)
                {
                    _hitsSpeedAttack = 0;
                    Bullet bulletScript = b.GetComponentInChildren<Bullet>();
                    bulletScript.Speed *= 2;
                }
            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        _allowShoot = false;
        yield return new WaitForSeconds(_timeBtwShots);
        _allowShoot = true;
    }
}
