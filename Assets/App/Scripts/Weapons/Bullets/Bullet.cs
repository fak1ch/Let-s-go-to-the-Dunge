using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    [SerializeField] private int _damage;
    [SerializeField] private bool _enemyBullet = false;
    [SerializeField] private bool _allowMove = true;
    [SerializeField] private AudioSoundAfterDestroy _sound;

    private bool _isOpen = true;
    private GameObject _whoseBullet;

    public float Speed { get => _speed; set => _speed = value; }
    public GameObject WhoseBullet => _whoseBullet;

    protected virtual void Start() 
    {
        StartCoroutine(DestroyAfterTime());
    }

    protected virtual void Update()
    {
        BulletMove();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter2DMethod(collision.gameObject);
    }

    private void OnTriggerEnter2DMethod(GameObject entity)
    {
        if (_isOpen)
        {
            if (!_enemyBullet)
            {
                if (entity.TryGetComponent(out Enemy script))
                {
                    _whoseBullet.GetComponent<PlayerStatistics>().AddDealtDamage(_damage);
                    BulletTouch(script);
                }
            }
            else if (_enemyBullet)
            {
                if (entity.TryGetComponent(out PlayerCharacteristic script))
                {
                    BulletTouch(script);
                }
            }
            if (entity.CompareTag("Ground") || entity.CompareTag("Wall") || entity.CompareTag("Door"))
            {
                BulletTouch(null);
            }
        }
    }

    protected virtual void BulletMove()
    {
        if(_allowMove)
        transform.Translate(new Vector3(1, 0, 0) * Speed * Time.deltaTime);
    }

    private void BulletTouch(Entity script)
    {
        _isOpen = false;
        if (script != null)
        script.TakeDamage(_damage, _whoseBullet);
        ExtraEffect();
        DestroyBullet();
    }

    protected virtual void ExtraEffect()
    {

    }

    public void SetWhoseBullet(GameObject player)
    {
        _whoseBullet = player;
    }

    private void DestroyBullet()
    {
        if (_sound != null)
        {
            _sound.PlaySoundAfterDestroy();
            Destroy(gameObject);
        }
        else
        {
            DestroyBulletForChild();
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_lifetime);
        DestroyBulletForChild();
    }

    public virtual void DestroyBulletForChild()
    {
        Destroy(transform.parent.gameObject);
    }
}
