using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazukaBullet : Bullet
{
    public GameObject bullet;

    // Update is called once per frame
    void Update()  
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        UpdateBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerBullet(collision);
    }

    public override void TriggerBullet(Collider2D collision)
    {
        if (!locker)
        {
            if (!enemy)
            {
                if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
                {
                    locker = true;
                    collision.GetComponent<Enemy>().TakeDamage(damage);
                    Boom();
                    Destroy(gameObject);
                }
            }
            else if (enemy)
            {
                if (collision.CompareTag("Player"))
                {
                    locker = true;
                    collision.GetComponent<PlayerCharacteristic>().TakeDamage(damage);
                    Boom();
                    Destroy(gameObject);
                }
            }
            if (collision.CompareTag("Ground") || collision.CompareTag("Wall") || collision.CompareTag("Door"))
            {
                locker = true;
                if (collision.GetComponent<VesselWithHealth>() != null)
                {
                    collision.GetComponent<VesselWithHealth>().SpawnHealth();
                }
                Boom();
                Destroy(gameObject);
            }
        }
    }

    private void Boom()
    {
        float angle = 0;
        for(int i = 0; i<=8; i++)
        {
            var b = Instantiate(bullet, transform.position, transform.rotation);
            b.GetComponentInChildren<Bullet>().speed = b.GetComponentInChildren<Bullet>().speed / 2;
            b.transform.Rotate(0.0f, 0.0f, angle);
            angle += 45;
        }
    }
}
