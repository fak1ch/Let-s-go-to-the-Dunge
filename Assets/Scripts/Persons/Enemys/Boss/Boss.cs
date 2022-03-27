using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : Enemy
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private GameObject _portal;

    private Transform _bossRoomTransform;
    private UIBossHp _bossHp;

    protected override void Start()
    {
        base.Start();
        _bossRoomTransform = GameObject.FindGameObjectWithTag("BossRoom").transform;
        _bossHp = FindObjectOfType<UIBossHp>();
        _bossHp.BossSpawned(_health, _maxHealth);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {

    }

    public override void TakeDamage(int damage, GameObject whoKill)
    {
        _health -= damage;
        _bossHp.TakeDamage(_health, _maxHealth);
        if (_health <= 0)
        {
            whoKill.GetComponent<PlayerStatistics>().AddCountEnemyDestroyed();
            EnemyHasBeenKilled();
        }
    }

    private void SpawnPortal()
    {
        Vector3 vec = _bossRoomTransform.position;
        vec.x = vec.x - 325;
        vec.y = vec.y - 100;
        Instantiate(_portal, vec, Quaternion.identity);
    }

    protected override void EnemyHasBeenKilled()
    {
        _bossHp.BossDie();
        SpawnPortal();
        _mainScript.MusicPlay(_mainScript.GetRandomMusic());

        base.EnemyHasBeenKilled();
    }
}
