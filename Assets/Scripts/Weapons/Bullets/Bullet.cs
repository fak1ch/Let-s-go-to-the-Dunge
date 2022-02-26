using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;
    public bool enemyBullet = false;
    [SerializeField] private bool _allowMove = true;
    public AudioSoundAfterDestroy sound;

    private bool _isOpen = true;

    protected void StartMethod()
    {
        StartCoroutine(DestroyAfterTime());
    }

    protected void OnCollisionEnter2DMethod(GameObject entity)
    {
        if (_isOpen)
        {
            if (!enemyBullet)
            {
                if (entity.CompareTag("Enemy") || entity.CompareTag("Boss"))
                {
                    BulletTouch(entity.GetComponent<Enemy>());
                }
            }
            else if (enemyBullet)
            {
                if (entity.CompareTag("Player"))
                {
                    BulletTouch(entity.GetComponent<PlayerCharacteristic>());
                }
            }
            if (entity.CompareTag("Ground") || entity.CompareTag("Wall") || entity.CompareTag("Door"))
            {
                BulletTouch(null);
            }
        }
    }

    protected void BulletMove()
    {
        if(_allowMove)
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
    }

    private void BulletTouch(IEntity script)
    {
        _isOpen = false;
        if (script != null)
        script.TakeDamage(damage);
        ExtraEffect();
        DestroyBullet();
    }

    public virtual void ExtraEffect()
    {

    }

    private void DestroyBullet()
    {
        if (sound != null)
        {
            sound.PlaySoundAfterDestroy();
            Destroy(gameObject);
        }
        else
        {
            DestroyBulletForChild();
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        DestroyBulletForChild();
    }

    public virtual void DestroyBulletForChild()
    {
        Destroy(transform.parent.gameObject);
    }
}
