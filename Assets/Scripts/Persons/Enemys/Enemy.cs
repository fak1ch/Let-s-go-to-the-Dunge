using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEntity
{
    public int health;
    public int damage;
    public float speed;
    public float timeBtwAttack;
    public NavMeshAgent navAgent;

    protected GameObject _player;
    protected PlayerCharacteristic _playerCharacteristic;
    protected MainScript _mainScript;
    protected Vector3 _startPosition;
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected bool _animHasBeenChanged = false;
    protected Vector3 _targetForMove;
    protected bool _lockerForAIMove;
    protected AudioSource _audioSource;

    private Collider2D _collider;
    private bool _isOpen = false;

    public void StartMethod()
    {
        _player = StaticClass.player;
        _playerCharacteristic = StaticClass.playerCharacteristic;
        _mainScript = StaticClass.mainScript;
        _startPosition = transform.position;
        StaticClass.mainScript.enemies.Add(this);
        _rb = gameObject.GetComponent<Rigidbody2D>();
        TryGetComponent(out _animator);
        TryGetComponent(out _audioSource);
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
        StartCoroutine(TimeBeforeAttackAfterSpawn());
    }

    protected void UpdateMethod()
    {
        EnemyMove();
        UpdateRotateSprite();
    }

    public void CollisionAttackPlayer(GameObject entity)
    {
        if (entity.CompareTag("Player"))
        {
            if (_isOpen)
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
        _playerCharacteristic.TakeDamage(damage);
        yield return new WaitForSeconds(timeBtwAttack);
        collider.enabled = true;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EnemyHasBeenKilled();
        }
    }

    private IEnumerator TimeBeforeAttackAfterSpawn()
    {
        yield return new WaitForSeconds(1f);
        _isOpen = true;
    }

    public void UpdateRotateSprite()
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
        if (!_lockerForAIMove)
        {
            StartCoroutine(TargetForMovePos());
        }
        if (_player.activeInHierarchy)
        {
            navAgent.SetDestination(_targetForMove);
            if (!_animHasBeenChanged && _animator != null)
            {
                _animHasBeenChanged = !_animHasBeenChanged;
                _animator.SetBool("isRun", true);
            }
        }
        else
        {
            MoveToStartPosition();
        }
    }

    public IEnumerator TargetForMovePos()
    {
        _lockerForAIMove = true;
        _targetForMove = _player.transform.position;
        float time = DistanceBetween2dPoints(_player.transform.position, transform.position)/1500;
        yield return new WaitForSeconds(time);
        _lockerForAIMove = false;
    }

    public virtual void MoveToStartPosition()
    {
        navAgent.SetDestination(_startPosition);
        if (_animHasBeenChanged && transform.position == _startPosition && _animator != null)
        {
            _animHasBeenChanged = !_animHasBeenChanged;
            _animator.SetBool("isRun", false);
        }
    }

    public virtual void EnemyHasBeenKilled()
    {
        StaticClass.mainScript.enemies.Remove(GetComponent<Enemy>());
        GetComponent<DropManaAndAmethistsAfterDeath>().DropManaAndAmethystAfterDead();
        Destroy(gameObject);
    }

    private float DistanceBetween2dPoints(Vector3 vec11, Vector3 vec22)
    {
        Vector2 vec1 = new Vector2(vec11.x, vec11.y);
        Vector2 vec2 = new Vector2(vec22.x, vec22.y);

        float distance;

        distance = Mathf.Pow(Mathf.Pow(vec2.x - vec1.x, 2) + Mathf.Pow(vec2.y - vec1.y, 2), 0.5f);

        return distance;
    }
}
