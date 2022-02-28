using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : Enemy
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _startTimeBtwShots;
    [SerializeField] private float _timeBeforeShoot;
    [SerializeField] private RunAwayPlayer _runScript;

    private float _timeBtwShots;
    private bool _allowShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        _startTimeBtwShots = Random.Range(_startTimeBtwShots - 1f, _startTimeBtwShots + 1f);
        _timeBtwShots = _startTimeBtwShots;
    }

    void Update()
    {
        UpdateMethod();

        if (_player.activeInHierarchy)
        {
            Vector3 vec = _mainScript.camera.WorldToScreenPoint(_player.transform.position);
            Vector3 objectPos = _mainScript.camera.WorldToScreenPoint(_shotPoint.position);
            vec.x = vec.x - objectPos.x;
            vec.y = vec.y - objectPos.y;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            _shotPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (_timeBtwShots <= 0 && _allowShoot)
            {
                _allowShoot = false;
                _runScript.enabled = false;
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionAttackPlayer(collision.gameObject);
    }

    private IEnumerator WaitTimeBeforeShoot()
    {
        Instantiate(_bullet, _bulletSpawnPoint.position, _shotPoint.rotation);
        ShotAudioPlay();
        AnimationChange();
        yield return new WaitForSeconds(_timeBeforeShoot);
        AnimationChange();
        _allowShoot = true;
        _runScript.enabled = true;
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

    private void ShotAudioPlay()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }
}
