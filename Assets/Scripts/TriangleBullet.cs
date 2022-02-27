using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBullet : Bullet
{
    [SerializeField] private GameObject _rotateBullet;
    [SerializeField] private float _rotateSpeed = 1;

    private void Start()
    {
        StartMethod();
    }
    void Update()
    {
        BulletMove();
        _rotateBullet.transform.Rotate(new Vector3(0, 0, 1) * _rotateSpeed);
    }

    public override void DestroyBulletForChild()
    {
        Destroy(gameObject);
    }
}
