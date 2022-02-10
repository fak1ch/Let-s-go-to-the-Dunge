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
    public IEnemy thisGameObjectScript;
    public Vector2 startPosition; 

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

    public void StartMethod(IEnemy script)
    {
        player = StaticClass.player;
        playerCharacteristic = StaticClass.playerCharacteristic;
        mainScript = StaticClass.mainScript;
        thisGameObjectScript = script;
        mainScript.enemies.Add(thisGameObjectScript);
        startPosition = transform.position;
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
            GetComponent<DropManaAfterDeath>().DropManaAfterDead();
            mainScript.enemies.Remove(thisGameObjectScript);
            Destroy(gameObject);
        }
    }

    public void MoveToSpawnPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
    }

    public void SetPlayerIsAlive(bool value)
    {
        playerIsAlive = value;
    }
}
