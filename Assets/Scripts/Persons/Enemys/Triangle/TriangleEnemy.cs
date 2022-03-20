using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : Enemy
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Transform _pointMove;
    [SerializeField] private Transform _pointRotate;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _startTimeBtwShots;
    [SerializeField] private float _timeBeforeShoot;
    [SerializeField] private float _timeBetweenGetDistance = 1f;
    [SerializeField] private float _distanceWhenEnemyMoveToPlayer = 500;

    private float _distance;
    private float _timeBtwShots;
    private bool _allowShoot = true;

    protected override void Start()
    {
        base.Start();
        _startTimeBtwShots = Random.Range(_startTimeBtwShots - 1f, _startTimeBtwShots + 1f);
        _timeBtwShots = _startTimeBtwShots;


        SetMoveBehaviour(new MoveToTarget(transform.root, _speed, transform.position));
        SetRotateBehaviour(new RotateToTarget(_mainScript.MainCamera));

        StartCoroutine(GetDistanceBetweenPlayerAndEnemy());
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
            Rotate(_player.transform.position, _shotPoint);
            float angle = Rotate(_player.transform.position, _pointRotate);
            if (_distance < _distanceWhenEnemyMoveToPlayer)
                _pointRotate.rotation = Quaternion.Euler(0, 0, angle - 180);
        }
    }

    private void Shoot()
    {
        if (_player.activeInHierarchy)
        {
            if (_timeBtwShots <= 0 && _allowShoot)
            {
                _allowShoot = false;
                StartCoroutine(WaitTimeBeforeShoot());
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public override void EnemyMove()
    {
        if (_allowShoot)
        Move(_pointMove.transform.position, _player.activeInHierarchy);
    }

    private IEnumerator WaitTimeBeforeShoot()
    {
        var bullet = Instantiate(_bullet, _bulletSpawnPoint.position, _shotPoint.rotation);
        ShotAudioPlay();
        AnimationChange();
        yield return new WaitForSeconds(_timeBeforeShoot);
        AnimationChange();
        _allowShoot = true;
        _timeBtwShots = _startTimeBtwShots;
    }

    private void AnimationChange()
    {
        if (!_animHasBeenChanged && _animator != null)
        {
            _animHasBeenChanged = !_animHasBeenChanged;
            _animator.SetBool("isRun", false);
            _animator.SetBool("isAttack", true);
        }
        else if (_animHasBeenChanged && _animator != null)
        {
            _animHasBeenChanged = !_animHasBeenChanged;
            _animator.SetBool("isRun", true);
            _animator.SetBool("isAttack", false);
        }
    }

    private IEnumerator GetDistanceBetweenPlayerAndEnemy()
    {
        _distance = Vector3.Distance(transform.root.position, _player.transform.position);
        yield return new WaitForSeconds(_timeBetweenGetDistance);
        StartCoroutine(GetDistanceBetweenPlayerAndEnemy());
    }

    private void ShotAudioPlay()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }
}
