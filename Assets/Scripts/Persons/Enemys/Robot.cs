using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy, IEnemy
{
    public GameObject bullet;
    public Transform shotPoint;
    public float startTimeBtwShots;

    private float timeBtwShots;
    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        EnemyMove();
        UpdateRotateSprite();

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
                Instantiate(bullet, shotPoint.position, shotPoint.rotation);

                timeBtwShots = startTimeBtwShots; 
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerAttackPlayer(collision);
    }
}
