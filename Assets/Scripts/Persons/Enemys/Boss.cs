using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _portal;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private Transform _bulletPoint2;
    [SerializeField] private int _bulletCount;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private AudioClip _runClip;

    private GameObject _bossRoom;
    private Transform _target;
    private float _angle;
    private bool _locked = false;
    private bool _lockedChoose = false;
    private UIBossHp _bossHp;

    void Start()
    {
        StartMethod();
        _bossHp = FindObjectOfType<UIBossHp>();
        _bossHp.StartHealth(health);
        _bossRoom = GameObject.FindWithTag("BossRoom");
        _target = _player.transform;
        if (!_lockedChoose)
        {
            StartCoroutine(ChooseAction());
        }
    }

    void FixedUpdate()
    {
        UpdateRotateSprite();
        SmartMenu();
    }

    private void SpawnPortal()
    {
        Vector3 vec = _bossRoom.transform.position;
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
                _target = _bossRoom.transform;
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
            transform.position = Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
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
            transform.position = Vector2.MoveTowards(transform.position, _startPosition, speed * Time.deltaTime);
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
            a.GetComponentInChildren<Bullet>().speed = b.GetComponentInChildren<Bullet>().speed / 2;
            b.transform.Rotate(0.0f, 0.0f, _angle);
            b.GetComponentInChildren<Bullet>().speed = b.GetComponentInChildren<Bullet>().speed / 1.5f;
            a.transform.Rotate(0.0f, 0.0f, _angle);
            
            _angle -= plusAngle;
            if (_bulletCount >= 40) { timeBtwAttack /= 2; }

            yield return new WaitForSeconds(timeBtwShots);
        }
        SetBoolShoot(false);
        SetBoolRun(true);
        _locked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionAttackPlayer(collision.gameObject);
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EnemyHasBeenKilled();
        }
        _bossHp.TakeDamage(damage);
    }

    private void SetBoolRun(bool value)
    {
        if (value)
        {
            _audioSource.Stop();
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
        GetComponent<DropManaAndAmethistsAfterDeath>().DropManaAndAmethystAfterDead();
        SpawnPortal();
        StaticClass.mainScript.MusicPlay(StaticClass.mainScript.kindOfMusicClips[Random.Range(0, StaticClass.mainScript.kindOfMusicClips.Count)]);
        _bossHp.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
