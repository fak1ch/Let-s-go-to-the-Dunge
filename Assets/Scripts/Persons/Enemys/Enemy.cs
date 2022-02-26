using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEntity
{
    public int health;
    public int damage;
    public float speed;
    public float timeBtwAttack;
    public NavMeshAgent navAgent;
    [HideInInspector] public GameObject player;
    [HideInInspector] public PlayerCharacteristic playerCharacteristic;
    [HideInInspector] public bool playerIsAlive = true;
    [HideInInspector] public MainScript mainScript;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool animHasBeenChanged = false;
    [HideInInspector] public Vector3 targetForMove;
    [HideInInspector] public bool lockerForAIMove;

    private Collider2D _collider;
    private bool _isOpen = false;

    public void StartMethod()
    {
        player = StaticClass.player;
        playerCharacteristic = StaticClass.playerCharacteristic;
        mainScript = StaticClass.mainScript;
        startPosition = transform.position;
        StaticClass.mainScript.enemies.Add(GetComponent<Enemy>());
        rb = gameObject.GetComponent<Rigidbody2D>();
        TryGetComponent(out animator);
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
        playerCharacteristic.TakeDamage(damage);
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
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        if (player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        Vector3 vec = transform.position;
        vec.z = 0;
        transform.position = vec;
    }

    public virtual void EnemyMove()
    {
        if (!lockerForAIMove)
        {
            StartCoroutine(TargetForMovePos());
        }
        if (playerIsAlive)
        {
            navAgent.SetDestination(targetForMove);
            if (!animHasBeenChanged && animator != null)
            {
                animHasBeenChanged = !animHasBeenChanged;
                animator.SetBool("isRun", true);
            }
        }
        else
        {
            MoveToStartPosition();
        }
    }

    public IEnumerator TargetForMovePos()
    {
        lockerForAIMove = true;
        targetForMove = player.transform.position;
        float time = DistanceBetween2dPoints(player.transform.position, transform.position)/1500;
        yield return new WaitForSeconds(time);
        lockerForAIMove = false;
    }

    public virtual void MoveToStartPosition()
    {
        navAgent.SetDestination(startPosition);
        if (animHasBeenChanged && transform.position == startPosition && animator != null)
        {
            animHasBeenChanged = !animHasBeenChanged;
            animator.SetBool("isRun", false);
        }
    }

    public void SetPlayerIsAlive(bool value)
    {
        playerIsAlive = value;
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
