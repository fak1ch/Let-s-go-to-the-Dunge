using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;
    public bool enemy = false;
    public AudioSoundAfterDestroy sound;

    private bool locker = false;

    public void UpdateBullet()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            DestroyBullet();
        }
    }

    public virtual void TriggerBullet(Collider2D collision)
    {
        if (!locker)
        {
            if (!enemy)
            {
                if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
                {
                    locker = true;
                    collision.GetComponent<Enemy>().TakeDamage(damage);
                    DestroyBullet();
                }
            }
            else if (enemy)
            {
                if (collision.CompareTag("Player"))
                {
                    locker = true;
                    collision.GetComponent<PlayerCharacteristic>().TakeDamage(damage);
                    DestroyBullet();
                }
            }
            if (collision.CompareTag("Ground") || collision.CompareTag("Wall") || collision.CompareTag("Door"))
            {
                locker = true;
                if (collision.GetComponent<VesselWithHealth>() != null)
                {
                    collision.GetComponent<VesselWithHealth>().SpawnHealth();
                }
                DestroyBullet();
            }
        }
    }

    private void DestroyBullet()
    {
        if (sound != null)
        {
            sound.PlaySoundAfterDestroy();
        }
        Destroy(gameObject);
    }
}
