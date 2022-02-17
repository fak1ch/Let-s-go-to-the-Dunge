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
            GetComponent<DropManaAfterDeath>().DropManaAfterDead();
            Destroy(gameObject);
        }
    }

    public virtual void EnemyMove()
    {
        if (playerIsAlive)
        {
            navAgent.SetDestination(player.transform.position);
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

    public void MoveToStartPosition()
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
}
