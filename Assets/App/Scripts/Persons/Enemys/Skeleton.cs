using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Start()
    {
        base.Start();
        SetMoveBehaviour(new NavMeshMoveToTarget(_navAgent, GetComponent<Animator>(), transform.position));
    }
}
