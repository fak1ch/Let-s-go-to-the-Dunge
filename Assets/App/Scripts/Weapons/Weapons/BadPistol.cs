using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPistol : Weapon
{
    protected override void Start()
    {
        IsDropped = false;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
