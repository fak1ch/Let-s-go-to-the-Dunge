using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy, IEnemy
{
    private GameObject bossRoom;
    private Transform target;
    public GameObject bullet;

    public int bulletCount;
    public Transform bulletPoint;
    public Transform bulletPoint2;
    private float angle;
    private float plusAngle;

    private bool locked = false;
    private bool lockedChoose = false;

    private Animator anim;
    public GameObject portal;
    public UIBossHp bossHp;

    void Start()
    {
        StartMethod(GetComponent<Boss>());
        bossHp = GameObject.FindGameObjectWithTag("BossHp").GetComponent<UIBossHp>();
        bossHp.StartHealth(health);
        anim = GetComponent<Animator>();
        bossRoom = GameObject.FindWithTag("BossRoom");
        target = player.transform;
        SetBoolShoot(false);
        SetBoolRun(false);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!lockedChoose)
        {
            StartCoroutine(ChooseAction());
        }
        UpdateRotateSprite();
        SmartMenu();
    }

    private void SpawnPortal()
    {
        Vector3 vec = bossRoom.transform.position;
        vec.x = vec.x - 325;
        Instantiate(portal, vec, Quaternion.identity);
    }

    private void SmartMenu()
    {
        if (GetBoolRun())
        {
            Run();
        }
        else if (GetBoolShoot())
        {
            if (!locked)
            {
                Shoot();
            }
        }
        else if(!GetBoolShoot() && !GetBoolRun())
        {

        }
    }

    IEnumerator ChooseAction()
    {
        lockedChoose = true;
        yield return new WaitForSeconds(1);
        SetBoolShoot(true);
        yield return new WaitForSeconds(3);
        for(; ; )
        {
            int i = Random.Range(4, 8);
            int k = Random.Range(0, 25);
            if(k==0)
            {
                target = bossRoom.transform;
            }
            yield return new WaitForSeconds(i);
            SetBoolShoot(true);
            target = player.transform;
        }
    }

    private void Run()
    {
        if (playerIsAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            if (transform.position.x == startPosition.x && transform.position.y == startPosition.y)
            {
                SetBoolShoot(false);
                SetBoolRun(false);
            }
            else
            {
                MoveToSpawnPosition();
            }
        }
    }

    private void Shoot()
    {
        if (playerIsAlive)
        {
            locked = true;
            StartCoroutine(Shoot1());
        }
    }

    IEnumerator Shoot1()
    {
        float timeBtwShots = 0.1f;
        bulletCount = Random.Range(8, 49);
        float plusAngle = 360 / bulletCount;

        for (int i = bulletCount; i >= 0; i--)
        {
            var b = Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity);
            var a = Instantiate(bullet, bulletPoint2.transform.position, Quaternion.identity);
            b.GetComponent<Bullet>().speed = b.GetComponent<Bullet>().speed / 1.5f;
            b.transform.Rotate(0.0f, 0.0f, angle);
            a.GetComponent<Bullet>().speed = b.GetComponent<Bullet>().speed / 1.5f;
            a.transform.Rotate(0.0f, 0.0f, angle);
            
            angle -= plusAngle;
            if (bulletCount >= 40) { timeBtwAttack /= 2; }

            yield return new WaitForSeconds(timeBtwShots);
        }
        SetBoolShoot(false);
        SetBoolRun(true);
        locked = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerAttackPlayer(collision);
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GetComponent<DropManaAfterDeath>().DropManaAfterDead();
            mainScript.enemies.Remove(thisGameObjectScript);
            SpawnPortal();
            Destroy(gameObject);
        }
        bossHp.TakeDamage(damage);
    }

    private void SetBoolRun(bool value)
    {
        if (value)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isShooting", false);
        }
        else if (!value)
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void SetBoolShoot(bool value)
    {
        if (value)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isShooting", true);
        }
        else if (!value)
        {
            anim.SetBool("isShooting", false);
        }
    }

    private bool GetBoolRun()
    {
        return anim.GetBool("isRunning");
    }

    private bool GetBoolShoot()
    {
        return anim.GetBool("isShooting");
    }

    public void BossDie()
    {
        SpawnPortal();
        Destroy(bossHp);
        Destroy(gameObject);
    }
}
