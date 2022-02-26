using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{

    private void Start()
    {
        StartMethod();
    }
    void Update()
    {
        BulletMove();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter2DMethod(collision.gameObject);
    }
}
