using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    public GameObject bullet;
    public Transform shotPoint;
    public float startTimeBtwShots;

    private float timeBtwShots;
    private int _hitsSpeedAttack;

    void Start()
    {
        StartMethod();
        startTimeBtwShots = Random.Range(startTimeBtwShots, startTimeBtwShots + 0.5f);
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        UpdateMethod();
        GunRotateToTarget();
        Shoot();
    }

    private void GunRotateToTarget()
    {
        if (_player.activeInHierarchy)
        {
            Vector3 vec = _mainScript.camera.WorldToScreenPoint(_player.transform.position);
            Vector3 objectPos = _mainScript.camera.WorldToScreenPoint(shotPoint.position);
            vec.x = vec.x - objectPos.x;
            vec.y = vec.y - objectPos.y;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            shotPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Shoot()
    {
        if (_player.activeInHierarchy)
        {
            if (timeBtwShots <= 0)
            {
                ShotAudioPlay();
                var b = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
                if (_hitsSpeedAttack == 5)
                {
                    _hitsSpeedAttack = 0;
                    Bullet bulletScript = b.GetComponentInChildren<Bullet>();
                    bulletScript.speed *= 2;
                }
                else
                {
                    _hitsSpeedAttack++;
                }

                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void ShotAudioPlay()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionAttackPlayer(collision.gameObject);
    }
}
