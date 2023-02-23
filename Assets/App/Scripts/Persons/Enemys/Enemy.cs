using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected float _timeBtwAttack = 1;
    [SerializeField] protected float _speed;
    [SerializeField] protected NavMeshAgent _navAgent;

    protected AudioSource _audioSource;

    private Collider2D _collider;
    private bool _isOpen = false;
    private Vector3 _target;
    private Vector3 _startPosition;

    protected override void Start()
    {
        base.Start();
        _startPosition = transform.position;
        if (_navAgent != null)
        _navAgent.speed = _speed;
        _mainScript.AddEnemyToList(this);
        TryGetComponent(out _audioSource);
        StartCoroutine(TimeBeforeAttackAfterSpawn());
        StartCoroutine(TargetForMovePos());
    }

    protected virtual void Update()
    {
        EnemyMove();
        RotateSprite();
    }

    protected virtual void EnemyMove()
    {
        if (transform.position != _startPosition)
            Move(_target, _player.activeInHierarchy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isOpen)
        {
            var entity = collision.gameObject;
            if (entity.TryGetComponent(out _playerCharacteristic))
            {
                if (_collider == null)
                {
                    _collider = GetComponent<Collider2D>();
                    _collider.enabled = false;
                    StartCoroutine(AttackThePlayer(_collider));
                }
                else if (_collider.enabled)
                {
                    _collider.enabled = false;
                    StartCoroutine(AttackThePlayer(_collider));
                }
            }
        }
    }

    public override void TakeDamage(int damage, GameObject killer)
    {
        _health -= damage;
        if (_health <= 0)
        {
            killer.GetComponent<PlayerStatistics>().AddCountEnemyDestroyed();
            EnemyHasBeenKilled();
        }
    }

    private IEnumerator TimeBeforeAttackAfterSpawn()
    {
        yield return new WaitForSeconds(1f);
        _isOpen = true;
    }

    private void RotateSprite()
    {
        if (_player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        if (_player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        Vector3 vec = transform.position;
        vec.z = 0;
        transform.position = vec;
    }

    private IEnumerator TargetForMovePos()
    {
        _target = _player.transform.position;
        float time = Vector2.Distance(_player.transform.position, transform.position) / 1500;
        yield return new WaitForSeconds(time);
        StartCoroutine(TargetForMovePos());
    }

    protected virtual void EnemyHasBeenKilled()
    {
        _health = 0;
        StaticClass.mainScript.RemoveEnemyFromList(this);
        GetComponent<DropManaAndAmethistsAfterDeath>().DropManaAndAmethystAfterDead();
        Destroy(transform.root.gameObject);
    }

    private IEnumerator AttackThePlayer(Collider2D collider)
    {
        _playerCharacteristic.TakeDamage(_damage, null);
        yield return new WaitForSeconds(_timeBtwAttack);
        collider.enabled = true;
    }

    
    protected virtual void ShotAudioPlay()
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource.Play();
        }
    }
}
