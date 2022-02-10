using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazukaBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;
    public GameObject bullet;

    private bool locked = false;

    // Update is called once per frame
    void Update()  
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            transform.Translate(-new Vector3(1, 0, 0) * speed * Time.deltaTime);
            Boom();
            Destroy(gameObject);
        }
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!locked)
        {
            if (collision.CompareTag("Enemy"))
            {
                locked = true;
                transform.Translate(-new Vector3(1, 0, 0) * speed * Time.deltaTime);
                collision.GetComponent<Enemy>().TakeDamage(damage);
                Boom();
                Destroy(gameObject);
            }
            if (collision.CompareTag("Boss"))
            {
                locked = true;
                transform.Translate(-new Vector3(1, 0, 0) * speed * Time.deltaTime);
                collision.GetComponent<Boss>().TakeDamage(damage);
                Boom();
                Destroy(gameObject);
            }
            if (collision.CompareTag("Ground") || collision.CompareTag("Wall") || collision.CompareTag("Door"))
            {
                locked = true;
                transform.Translate(-new Vector3(1, 0, 0) * speed * Time.deltaTime);
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
            b.GetComponent<Bullet>().speed = b.GetComponent<Bullet>().speed / 2;
            b.transform.Rotate(0.0f, 0.0f, angle);
            angle += 45;
        }
    }
}
