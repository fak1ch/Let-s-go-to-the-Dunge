using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    private void Start()
    {
        StartMethod();
    }
    private void Update()
    {
        BulletMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter2DMethod(collision.gameObject);
    }
}
