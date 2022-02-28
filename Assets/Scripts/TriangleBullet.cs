using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBullet : Bullet
{
    [SerializeField] private GameObject _rotateBullet;
    [SerializeField] private float _rotateSpeed = 1;
    private bool _allowMove = false;

    private void Start()
    {
        StartCoroutine(AnimationStop());
        StartMethod();
    }
    void Update()
    {
        if (_allowMove)
        {
            BulletMove();
            _rotateBullet.transform.Rotate(new Vector3(0, 0, 1) * _rotateSpeed);
        }
    }

    public override void DestroyBulletForChild()
    {
        Destroy(gameObject);
    }

    private IEnumerator AnimationStop()
    {
        yield return new WaitForSeconds(1.5f);
        _allowMove = true;
    }
}
