using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    private Rigidbody2D rb;
    private GameObject player;
    public bool enemy = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (!enemy)
            {
                if (collision.CompareTag("Enemy"))
                {
                    collision.GetComponent<Enemy>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                if (collision.CompareTag("Boss"))
                {
                    collision.GetComponent<Boss>().TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            else if (enemy){
                if (collision.CompareTag("Player"))
                {
                    collision.GetComponent<MoveHeroe>().TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            if (collision.CompareTag("Ground") || collision.CompareTag("Wall") || collision.CompareTag("Door"))
            {
                Destroy(gameObject);
            }
            if (collision.GetComponent<VesselWithHealth>() != null)
            {
                collision.GetComponent<VesselWithHealth>().SpawnHealth();
            }
        }
    }
}
