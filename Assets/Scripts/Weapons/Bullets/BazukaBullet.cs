using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazukaBullet : Bullet
{
    [SerializeField] private GameObject _bullet;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void ExtraEffect()
    {
        Boom();
    }

    private void Boom()
    {
        float angle = 0;
        List<Bullet> list = new List<Bullet>();
        for(int i = 0; i<=8; i++)
        {
            var b = Instantiate(_bullet, transform.position, transform.rotation);
            list.Add(b.GetComponentInChildren<Bullet>());
            list[i].Speed /= 2;
            list[i].SetWhoseBullet(WhoseBullet);
            b.transform.Rotate(0.0f, 0.0f, angle);
            angle += 45;
        }
    }
}
