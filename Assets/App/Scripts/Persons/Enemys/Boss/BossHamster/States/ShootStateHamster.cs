using System.Collections;
using UnityEngine;

public class ShootStateHamster : State
{
    private BossHamsterAnimationController _animationController;
    private GameObject _bullet;
    private int _bulletCount = 40;
    private Transform _shotPoint1;
    private Transform _shotPoint2;
    private int _shotsCount = 0;
    private float _angle = 360;
    private float _timeBtwShots;
    private float _minusAngle = 5;
    private float _startTimeBtwShots = 0.1f;

    public ShootStateHamster(BossHamsterAnimationController animationController, Transform shotPoint1, Transform shotPoint2, GameObject bullet)
    {
        _animationController = animationController;
        _shotPoint1 = shotPoint1;
        _shotPoint2 = shotPoint2;
        _bullet = bullet;
    }

    public override void Enter()
    {
        _animationController.StateShoot();
        _bulletCount = Random.Range(8, 48);
        _startTimeBtwShots = 1 / ((float)_bulletCount / 2);
        _minusAngle = 360 / ((float)_bulletCount);
        _timeBtwShots = _startTimeBtwShots;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (_shotsCount < _bulletCount)
        {
            if (_timeBtwShots <= 0)
            {
                _timeBtwShots = _startTimeBtwShots;
                Shoot();
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }
        }
        else
        {
            Enter();
            _shotsCount = 0;
        }
    }

    private void Shoot()
    {
        var b = Instantiate(_bullet, _shotPoint1.transform.position, Quaternion.identity);
        var a = Instantiate(_bullet, _shotPoint2.transform.position, Quaternion.identity);
        b.transform.Rotate(0.0f, 0.0f, _angle);
        a.transform.Rotate(0.0f, 0.0f, _angle);

        _angle -= _minusAngle;
        _shotsCount++;
    }
}