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

    private void FixedUpdate()
    {
        if (playerIsAlive)
        {
            rb.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
        }
        else
        {
            MoveToSpawnPosition();
        }
        UpdateRotateSprite();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerAttackPlayer(collision); 
    }
}
