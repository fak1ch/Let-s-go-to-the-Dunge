using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        UpdateBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerBullet(collision);
    }
}
