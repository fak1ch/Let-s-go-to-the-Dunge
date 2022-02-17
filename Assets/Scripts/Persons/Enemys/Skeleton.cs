using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Skeleton : Enemy, IEnemy
{
    void Start()
    {
        StartMethod();
    }
    private void Update()
    {
        EnemyMove();
        UpdateRotateSprite();
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerAttackPlayer(collision); 
    }
}
