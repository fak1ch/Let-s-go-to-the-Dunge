using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazukaBullet : Bullet
{
    [SerializeField] private GameObject _bullet;

    private void Start()
    {
        StartMethod();
    }
    // Update is called once per frame
    private void Update()  
    {
        BulletMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter2DMethod(collision.gameObject);
    }

    public override void ExtraEffect()
    {
        transform.Translate(new Vector3(-2, 0, 0) * Speed * Time.deltaTime);
        Boom();
    }

    private void Boom()
    {
        float angle = 0;
        for(int i = 0; i<=8; i++)
        {
            var b = Instantiate(_bullet, transform.position, transform.rotation);
            b.GetComponentInChildren<Bullet>().Speed = b.GetComponentInChildren<Bullet>().Speed / 2;
            b.transform.Rotate(0.0f, 0.0f, angle);
            angle += 45;
        }
    }
}
