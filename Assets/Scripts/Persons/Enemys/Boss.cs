using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _portal;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private Transform _bulletPoint2;
    [SerializeField] private int _bulletCount;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private AudioClip _runClip;

    private Transform _bossRoomTransform;
    private Transform _target;
    private float _angle;
    private bool _locked = false;
    private bool _lockedChoose = false;
    private UIBossHp _bossHp;

    protected override void Start()
    {
        base.Start();
        _bossRoomTransform = GameObject.FindGameObjectWithTag("BossRoom").transform;
        _bossHp = FindObjectOfType<UIBossHp>();
        _bossHp.BossSpawned(_health, _maxHealth);
        _target = _player.transform;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (!_lockedChoose)
        {
            StartCoroutine(ChooseAction());
        }
    }

    protected override void Update()
    {
        RotateSprite();
        SmartMenu();
    }

    public override void TakeDamage(int damage, GameObject whoKill)
    {
        _health -= damage;
        _bossHp.TakeDamage(_health, _maxHealth);
        if (_health <= 0)
        {
            whoKill.GetComponent<PlayerStatistics>().AddCountEnemyDestroyed();
            EnemyHasBeenKilled();
        }
    }

    private void SpawnPortal()
    {
        Vector3 vec = _bossRoomTransform.position;
        vec.x = vec.x - 325;
        vec.y = vec.y - 100;
        Instantiate(_portal, vec, Quaternion.identity);
    }

    private void SmartMenu()
    {
        if (GetBoolRun() || !_player.activeInHierarchy)
        {
            EnemyMove();
        }
        else if (GetBoolShoot())
        {
            if (!_locked)
            {
                Shoot();
            }
        }
        else if(!GetBoolShoot() && !GetBoolRun())
        {

        }
    }

    IEnumerator ChooseAction()
    {
        _lockedChoose = true;
        yield return new WaitForSeconds(1);
        SetBoolRun(false);
        SetBoolShoot(true);
        yield return new WaitForSeconds(3);
        for(; ; )
        {
            int i = Random.Range(4, 8);
            int k = Random.Range(0, 25);
            if(k==0)
            {
                _target = _bossRoomTransform;
            }
            yield return new WaitForSeconds(i);
            if (_player.activeInHierarchy)
            {
                SetBoolShoot(true);
                SetBoolRun(false);
            }
            _target = _player.transform;
        }
    }

    public override void EnemyMove()
    {
        if (_player.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
        else
        {
            MoveToStartPosition();
        }
    }

    public override void MoveToStartPosition()
    {
        if (transform.position != _startPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPosition, _speed * Time.deltaTime);
        }
        else
        {
            SetBoolShoot(false);
            SetBoolRun(false);
        }
    }

    private void Shoot()
    {
        _locked = true;
        StartCoroutine(Shoot1());
    }

    private IEnumerator Shoot1()
    {
        float timeBtwShots = 0.1f;
        _bulletCount = Random.Range(8, 49);
        float plusAngle = 360 / _bulletCount;

        for (int i = _bulletCount; i >= 0; i--)
        {
            var b = Instantiate(_bullet, _bulletPoint.transform.position, Quaternion.identity);
            var a = Instantiate(_bullet, _bulletPoint2.transform.position, Quaternion.identity);
            Bullet aBullet = a.GetComponentInChildren<Bullet>();
            Bullet bBullet = b.GetComponentInChildren<Bullet>();
            aBullet.Speed = aBullet.Speed / 2;
            b.transform.Rotate(0.0f, 0.0f, _angle);
            bBullet.Speed = bBullet.Speed / 2;
            a.transform.Rotate(0.0f, 0.0f, _angle);
            
            _angle -= plusAngle;
            if (_bulletCount >= 40) { _timeBtwAttack /= 2; }

            yield return new WaitForSeconds(timeBtwShots);
        }
        SetBoolShoot(false);
        SetBoolRun(true);
        _locked = false;
    }

    private void SetBoolRun(bool value)
    {
        if (value)
        {
            _audioSource.Stop();
            _audioSource.volume = 1f;
            _audioSource.clip = _runClip;
            _audioSource.Play();
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isShooting", false);
        }
        else if (!value)
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void SetBoolShoot(bool value)
    {
        if (value)
        {
            _audioSource.Stop();
            _audioSource.volume = 0.7f;
            _audioSource.clip = _shootClip;
            _audioSource.Play();
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isShooting", true);
        }
        else if (!value)
        {
            _animator.SetBool("isShooting", false);
        }
    }

    private bool GetBoolRun()
    {
        return _animator.GetBool("isRunning");
    }

    private bool GetBoolShoot()
    {
        return _animator.GetBool("isShooting");
    }

    public override void EnemyHasBeenKilled()
    {
        _health = 0;
        _bossHp.BossDie();
        GetComponent<DropManaAndAmethistsAfterDeath>().DropManaAndAmethystAfterDead();
        SpawnPortal();
        _mainScript.MusicPlay(_mainScript.GetRandomMusic());
        Destroy(gameObject);
    }
}
