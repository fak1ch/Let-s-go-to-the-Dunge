using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy, IEnemy
{
    public GameObject bullet;
    public Transform shotPoint;
    public float startTimeBtwShots;

    private float timeBtwShots;
    private int _hitsSpeedAttack;
    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        startTimeBtwShots = Random.Range(startTimeBtwShots, startTimeBtwShots + 0.5f);
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        UpdateMethod();

        if (playerIsAlive)
        {
            Vector3 vec = mainScript.camera.WorldToScreenPoint(player.transform.position);
            Vector3 objectPos = mainScript.camera.WorldToScreenPoint(shotPoint.position);
            vec.x = vec.x - objectPos.x;
            vec.y = vec.y - objectPos.y;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            shotPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (timeBtwShots <= 0)
            {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionAttackPlayer(collision.gameObject);
    }
}
