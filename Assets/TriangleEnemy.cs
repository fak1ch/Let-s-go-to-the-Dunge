using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : Enemy, IEnemy
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _startTimeBtwShots;
    [SerializeField] private float _timeBeforeShoot;

    private float _timeBtwShots;
    private bool _allowShoot = true;
    private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        StartCoroutine(NewPosForAIMove());
        _startTimeBtwShots = Random.Range(_startTimeBtwShots, _startTimeBtwShots + 0.5f);
        _timeBtwShots = _startTimeBtwShots;
    }

    void Update()
    {
        UpdateMethod();

        if (playerIsAlive)
        {
            Vector3 vec = mainScript.camera.WorldToScreenPoint(player.transform.position);
            Vector3 objectPos = mainScript.camera.WorldToScreenPoint(_shotPoint.position);
            vec.x = vec.x - objectPos.x;
            vec.y = vec.y - objectPos.y;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            _shotPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (_timeBtwShots <= 0 && _allowShoot)
            {
                _allowShoot = false;
                StartCoroutine(WaitTimeBeforeShoot());
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public override void EnemyMove()
    {
        if (playerIsAlive)
        {
            if (_timeBtwShots > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos , speed * Time.deltaTime);
                if (animHasBeenChanged && animator != null)
                {
                    animHasBeenChanged = !animHasBeenChanged;
                    animator.SetBool("isAttack", false);
                    animator.SetBool("isRun", true);
                }
            }
        }
        else
        {
            MoveToStartPosition();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionAttackPlayer(collision.gameObject);
    }

    private IEnumerator WaitTimeBeforeShoot()
    {
        if (!animHasBeenChanged && animator != null)
        {
            animHasBeenChanged = !animHasBeenChanged;
            animator.SetBool("isRun", false);
            animator.SetBool("isAttack", true);
        }
        yield return new WaitForSeconds(_timeBeforeShoot);
        Instantiate(_bullet, _bulletSpawnPoint.position, _shotPoint.rotation);
        _allowShoot = true;
        _timeBtwShots = _startTimeBtwShots;
    }

    private IEnumerator NewPosForAIMove()
    {
        float distance = DistanceBetween2dPoints(transform.position, player.transform.position);
        if (distance >= 500) { targetPos = player.transform.position; }
        else { targetPos = -player.transform.position; }
        targetPos.x += Random.Range(-200, +200);
        targetPos.y += Random.Range(-200, +200);
        yield return new WaitForSeconds(2f);
        StartCoroutine(NewPosForAIMove());
    }

    private float DistanceBetween2dPoints(Vector2 vec1, Vector2 vec2)
    {
        float distance;

        distance = Mathf.Pow(Mathf.Pow(vec2.x - vec1.x, 2) + Mathf.Pow(vec2.y - vec1.y, 2), 0.5f);

        return distance;
    }
}
