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

    private bool locked = false;
    private bool lockedChoose = false;

    public GameObject portal;
    public UIBossHp bossHp;

    void Start()
    {
        StartMethod();
        bossHp = GameObject.FindGameObjectWithTag("BossHp").GetComponent<UIBossHp>();
        bossHp.StartHealth(health);
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
        vec.y = vec.y - 100;
        Instantiate(portal, vec, Quaternion.identity);
    }

    private void SmartMenu()
    {
        if (GetBoolRun())
        {
            EnemyMove();
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

    public override void EnemyMove()
    {
        if (!lockerForAIMove)
        {
            StartCoroutine(TargetForMovePos());
        }
        if (playerIsAlive)
        {
            navAgent.SetDestination(targetForMove);
            SetBoolShoot(false);
            SetBoolRun(false);
        }
        else
        {
            MoveToStartPosition();
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
            a.GetComponentInChildren<Bullet>().speed = b.GetComponentInChildren<Bullet>().speed / 2;
            b.transform.Rotate(0.0f, 0.0f, angle);
            b.GetComponentInChildren<Bullet>().speed = b.GetComponentInChildren<Bullet>().speed / 1.5f;
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
            SpawnPortal();
            bossHp.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        bossHp.TakeDamage(damage);
    }

    private void SetBoolRun(bool value)
    {
        if (value)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isShooting", false);
        }
        else if (!value)
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void SetBoolShoot(bool value)
    {
        if (value)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isShooting", true);
        }
        else if (!value)
        {
            animator.SetBool("isShooting", false);
        }
    }

    private bool GetBoolRun()
    {
        return animator.GetBool("isRunning");
    }

    private bool GetBoolShoot()
    {
        return animator.GetBool("isShooting");
    }

    public void BossDie()
    {
        SpawnPortal();
        Destroy(bossHp);
        Destroy(gameObject);
    }
}
