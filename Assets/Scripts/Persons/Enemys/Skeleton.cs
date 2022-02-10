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
        StartMethod(GetComponent<Skeleton>());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerIsAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
