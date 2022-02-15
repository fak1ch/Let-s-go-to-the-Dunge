using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Vector2 startPosition;
    public Rigidbody2D rb;

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

    public void StartMethod()
    {
        player = StaticClass.player;
        playerCharacteristic = StaticClass.playerCharacteristic;
        mainScript = StaticClass.mainScript;
        startPosition = transform.position;
        StaticClass.mainScript.enemies.Add(GetComponent<Enemy>());
        rb = gameObject.GetComponent<Rigidbody2D>();
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

    public void MoveToSpawnPosition()
    {
        rb.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.fixedDeltaTime);
    }

    public void SetPlayerIsAlive(bool value)
    {
        playerIsAlive = value;
    }
}
