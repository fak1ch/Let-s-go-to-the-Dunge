using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Skeleton : Enemy
{
    void Start()
    {
        StartMethod();
    }

    private void Update()
    {
        UpdateMethod();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionAttackPlayer(collision.gameObject);
    }
}
