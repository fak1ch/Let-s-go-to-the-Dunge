using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _timeBtwAttack;
    [SerializeField] protected NavMeshAgent _navAgent;

    protected GameObject _player;
    protected PlayerCharacteristic _playerCharacteristic;
    protected MainScript _mainScript;
    protected AudioSource _audioSource;

    private Vector3 _target;
    private bool _lockerForAIMove;
    private Collider2D _collider;
    private bool _isOpen = false;

    protected override void Start()
    {
        base.Start();
        _player = StaticClass.player;
        _playerCharacteristic = StaticClass.playerCharacteristic;
        _mainScript = StaticClass.mainScript;
        _mainScript.AddEnemyToList(this);
        TryGetComponent(out _audioSource);
        StartCoroutine(TimeBeforeAttackAfterSpawn());
    }

    protected virtual void Update()
    {
        EnemyMove();
        RotateSprite();
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

    private IEnumerator AttackThePlayer(Collider2D collider)
    {
        _playerCharacteristic.TakeDamage(_damage, null);
        yield return new WaitForSeconds(_timeBtwAttack);
        collider.enabled = true;
    }

    public override void TakeDamage(int damage, GameObject whoKill)
    {
        _health -= damage;
        if (_health <= 0)
        {
            whoKill.GetComponent<PlayerStatistics>().AddCountEnemyDestroyed();
            EnemyHasBeenKilled();
        }
    }

    private IEnumerator TimeBeforeAttackAfterSpawn()
    {
        yield return new WaitForSeconds(1f);
        _isOpen = true;
    }

    public void RotateSprite()
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

    public virtual void EnemyMove()
    {
        Move(_target, _player.activeInHierarchy);
        if (!_lockerForAIMove)
        {
            StartCoroutine(TargetForMovePos());
        }
    }

    public IEnumerator TargetForMovePos()
    {
        _lockerForAIMove = true;
        _target = _player.transform.position;
        float time = Vector2.Distance(_player.transform.position, transform.position)/1500;
        yield return new WaitForSeconds(time);
        _lockerForAIMove = false;
    }

    public virtual void EnemyHasBeenKilled()
    {
        _health = 0;
        StaticClass.mainScript.RemoveEnemyFromList(this);
        GetComponent<DropManaAndAmethistsAfterDeath>().DropManaAndAmethystAfterDead();
        Destroy(gameObject.transform.root.gameObject);
    }
}
