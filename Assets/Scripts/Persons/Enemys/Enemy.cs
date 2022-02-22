using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public PlayerCharacteristic playerCharacteristic;
    public int health;
    public int damage;
    public float speed;
    public float timeBtwAttack;
    public bool playerIsAlive = true;
    public MainScript mainScript;
    public Vector3 startPosition;
    public Rigidbody2D rb;
    public Animator animator;
    public bool animHasBeenChanged = false;
    public NavMeshAgent navAgent;

    public Vector3 targetForMove;
    public bool lockerForAIMove = false;

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

    public void TriggerAttackPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.GetComponent<Collider2D>().enabled)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(AttackThePlayer(gameObject.GetComponent<Collider2D>()));
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
            StaticClass.mainScript.enemies.Remove(GetComponent<Enemy>());
            GetComponent<DropManaAndAmethistsAfterDeath>().DropManaAndAmethystAfterDead();
            Destroy(gameObject);
        }
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

    private float DistanceBetween2dPoints(Vector2 vec1, Vector2 vec2)
    {
        float distance;

        distance = Mathf.Pow(Mathf.Pow(vec2.x - vec1.x, 2) + Mathf.Pow(vec2.y - vec1.y, 2), 0.5f);

        return distance;
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
