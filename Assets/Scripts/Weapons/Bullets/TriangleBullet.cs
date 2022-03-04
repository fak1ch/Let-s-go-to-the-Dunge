using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBullet : Bullet
{
    [SerializeField] private GameObject _rotateBullet;
    [SerializeField] private float _rotateSpeed = 1;
    private bool _allowMove = false;
    private GameObject _player;
    private MainScript _mainScript;

    private void Start()
    {
        _player = StaticClass.player;
        _mainScript = StaticClass.mainScript;
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
        else
        {
            RotateBulletAfterShoot();
        }
    }

    private void RotateBulletAfterShoot()
    {
        if (_player.activeInHierarchy)
        {
            Vector3 vec = _mainScript.MainCamera.WorldToScreenPoint(_player.transform.position);
            Vector3 objectPos = _mainScript.MainCamera.WorldToScreenPoint(transform.position);
            vec.x = vec.x - objectPos.x;
            vec.y = vec.y - objectPos.y;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
